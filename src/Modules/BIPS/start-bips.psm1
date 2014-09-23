$dtsns = 'www.microsoft.com/SqlServer/Dts';

function convertto-packageXml
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [Microsoft.SqlServer.Dts.Runtime.Package]
        # the package to convert to XML
        $package,

        [Parameter()]
        [switch]
        # when specified, the XML is returned as a string
        $asString
    );

    process {
        [string] $xmlString = '';
        $package.SaveToXml( [ref]$xmlString, $null );

        if( $asString )
        {
            return $xmlString;
        }

        [xml]$xmlString;
    }

<# 
   .SYNOPSIS 
    Converts an existing SSIS package object to XML.
   .DESCRIPTION
    Converts an existing SSIS package object to XML.
   .EXAMPLE 
    get-item myServer:/packages/myPackage | convertto-packageXml

    Converts the package object found at the BIPS drive location to it's XML equivalent
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 14:08:57 
#> 
}

function update-packageFromXml
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [Microsoft.SqlServer.Dts.Runtime.Package]
        # the existing package to update
        $package,

        [Parameter(Mandatory=$true, Position=1)]
        [xml]
        # the package xml to apply to the package
        $xml
    );

    process {
        [string] $xmlString = $xml.OuterXml;
        $package.LoadFromXml( $xmlString, $null );
    }
<# 
   .SYNOPSIS 
    Updates an existing SSIS package object from XML.
   .DESCRIPTION
    Updates an existing SSIS package object from XML.
   .EXAMPLE 
    get-item myServer:/packages/myPackage | update-packageFromXml -xml $xml

    Updates the package object found at the BIPS drive location from the XML document
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 14:08:57 
#> 
}

function convertfrom-packageXml
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [CodeOwls.Bips.Utility.XmlFile()]
        [xml]
        # the package xml to convert to an object
        $xml
    );

    process {
        [string] $xmlString = $xml.OuterXml;
        $package = new-object Microsoft.SqlServer.Dts.Runtime.Package;
        $package.LoadFromXml( $xmlString, $null );

        $package;
    }
<# 
   .SYNOPSIS 
    Creates a new SSIS package object from XML.
   .DESCRIPTION
    Creates a new SSIS package object from XML.
   .EXAMPLE 
    $package = convertfrom-packageXml $xml

    Creates a new SSIS package object from the XML document
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 14:08:57 
#> 
}

function save-package
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [CodeOwls.BIPS.Utility.PackageDescriptor]
        # the package to save
        $package
    );

    process {
        $pkg = $package;
        $path = $package.Location;
        $ssisApplication.SaveToXml( $path, $pkg, $null );
    }

<# 
   .SYNOPSIS 
    Saves the specified package to the local file system.
   .DESCRIPTION
    Saves the specified package to the local file system.
   .EXAMPLE 
    get-item myServer:/packages/myPackage | save-package

    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 14:08:57 
#> 
}

function reset-designTimeLayout
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [CodeOwls.Bips.Utility.XmlFile()]
        [xml]
        # the package XML to auto-layout
        $xml,

        [Parameter(ParameterSetName='package')]
        [switch]
        # when specified, drops the package back on the pipeline
        $passThru
    );

    process {

        $version = $xml | select-packageXmlNode "/dts:Executable/@dts:ExecutableType";
        switch -Regex ( $version.Value )
        {
            '2$' { 
                $xml | select-packageXmlNode "//dts:PackageVariable[ ./dts:Property[ @dts:Namespace='dts-designer-1.0' ] ]" | foreach {
                    $_.parentNode.removeChild( $_ );                        
                }
                break;
            }

            '3$' {
                $xml | select-packageXmlNode "/dts:Executable/dts:DesignTimeProperties" | foreach {
                    $_.parentNode.removeChild( $_ );
                }
                break;
            }
            default {
                write-warning "Unknown package version: $($version.Value)"
            }
        }

        $xml;
    }

<# 
   .SYNOPSIS 
    removes all DTS layout formatting from the package
   .DESCRIPTION
    removes all DTS layout formatting from the package
   .EXAMPLE 
    get-item myServer:/packages/myPackage | reset-designTimeLayout

    Removes all DTS layout information from the package at the specified BIPS location
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 17:28:44 
#> 
}

function get-packageXml
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [alias("pspath")]
        [alias("fullname")]
        # the package to load
        $path
    );
    
    process {
        $x = [xml]( get-content $path );
        
        $e = $x.CreateElement("DTS","Property", $dtsns );
        $v = $x.CreateTextNode($path);
        $a = $x.CreateAttribute("DTS","Name", $dtsns );
        $a.Value = "BIPSOriginalFilePath";
        $e.AppendChild($v) | Out-Null;
        $e.Attributes.Append($a)| Out-Null;
        $x.DocumentElement.PrependChild( $e )| Out-Null;

        $x;
    }

<# 
   .SYNOPSIS 
    loads the specified package XML file
   .DESCRIPTION
    loads the specified package XML file
   .EXAMPLE 
    ls *.dtsx | get-pacakgeXml
    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 17:28:44 
#> 
}


function save-packageXml
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [xml]
        # the package to load
        $xml,

        [Parameter()]
        [alias("destination")]
        [string]
        # the file or folder location to output the package XML; if unspecified, defaults to the current filesystem location
        $outputPath = (Get-Location -PSProvider filesystem),

        [Parameter()]
        [switch]
        $passthru
    );
    
    process {
        $originalFilePathNode = $xml | select-packageXmlNode "//dts:Property[ @dts:Name='BIPSOriginalFilePath' ]";
        $originalFilePath = $originalFilePathNode.'#text';
        $originalFilePathNode.ParentNode.RemoveChild($originalFilePathNode) | out-null;

        write-debug "Original BIPS file path: $originalFilePath"

        $path =  $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($outputPath);
        if( -not [io.file]::exists( $path ) )
        {
            $filename = (split-path $originalFilePath -Leaf)
            $path = $path | join-path -child $filename
        }

        write-debug "Saving XML document to file path: $path"
        $xml.Save( $path );

        if($passthru)
        {
            $xml;
        }
    }

<# 
   .SYNOPSIS 
    saves the specified package XML file
   .DESCRIPTION
    saves the specified package XML file
   .EXAMPLE 
    ls *.dtsx | disable-validation | save-pacakgeXml
    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 17:28:44 
#> 
}

function disable-config
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [CodeOwls.BIPS.Utility.XmlFile()]
        [xml]
        # the package to process
        $xml
    );
    
    process {
        $xml | select-packageXmlNode "//dts:Property[@dts:Name='EnableConfig']" | foreach {
            $_.'#text' = '0';
        }

        $xml;
    }

<# 
   .SYNOPSIS 
    disables the configuration system for the package XML file
   .DESCRIPTION
    disables the configuration system for the package XML file
   .EXAMPLE 
    ls *.dtsx | disable-config
    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/22/2014 23:34:21 
#> 
}

function enable-config
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [CodeOwls.BIPS.Utility.XmlFile()]
        [xml]
        # the package to process
        $xml
    );
    
    process {
        $xml | select-packageXmlNode "//dts:Property[@dts:Name='EnableConfig']" | foreach {
            $_.'#text' = '-1';
        }

        $xml;
    }

<# 
   .SYNOPSIS 
    enables the configuration system for the package XML file
   .DESCRIPTION
    enables the configuration system for the package XML file
   .EXAMPLE 
    ls *.dtsx | enable-config
    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/22/2014 23:34:21 
#> 
}

function enable-package
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [CodeOwls.BIPS.Utility.XmlFile()]
        [xml]
        # the package to enable
        $xml
    );
    
    process {
        $xml | select-packageXmlNode "//dts:Property[@dts:Name='Disabled']" | foreach {
            $_.'#text' = '0';
        }

        $xml;
    }

<# 
   .SYNOPSIS 
    enables the package 
   .DESCRIPTION
    enables the package 
   .EXAMPLE 
    ls *.dtsx | enable-package
    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/22/2014 23:34:21 
#> 
}

function disable-package
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [CodeOwls.BIPS.Utility.XmlFile()]
        [xml]
        # the package to disable
        $xml
    );
    
    process {
        $xml | select-packageXmlNode "//dts:Property[@dts:Name='Disabled']" | foreach {
            $_.'#text' = '-1';
        }

        $xml;
    }

<# 
   .SYNOPSIS 
    disables the package 
   .DESCRIPTION
    disables the package 
   .EXAMPLE 
    ls *.dtsx | disable-package
    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/22/2014 23:34:21 
#> 
}

function disable-validation
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [CodeOwls.BIPS.Utility.XmlFile()]
        [xml]
        # the package to auto-layout
        $xml
    );
    
    process {
        $xml | select-packageXmlNode "//dts:Property[@dts:Name='DelayValidation']" | foreach {
             $_.'#text' = '-1';
        }

        $xml;
    }

<# 
   .SYNOPSIS 
    disables all validations in the package XML file
   .DESCRIPTION
    disables all validations in the package XML file
   .EXAMPLE 
    ls *.dtsx | disable-validation
    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 17:28:44 
#> 
}

function enable-validation
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [CodeOwls.Bips.Utility.XmlFile()]
        [xml]
        # the package to auto-layout
        $xml
    );
    
    process {
        $xml |  select-packageXmlNode "//dts:Property[@dts:Name='DelayValidation']" | foreach {
            $_.'#text' = '0';
        }

        $xml;
    }

<# 
   .SYNOPSIS 
    enables all validations in the package XML file
   .DESCRIPTION
    enables all validations in the package XML file
   .EXAMPLE 
    ls *.dtsx | disable-validation
    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 17:28:44 
#> 
}

function find-componentMissingInputColumn
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [CodeOwls.Bips.Utility.XmlFile()]
        # the path of the package to process
        $xml
    );

    process {
        $xml | select-packageXmlNode "//component[ ./inputs/input[ not( ./inputColumns ) ] ]" | foreach {
            new-object psobject -prop @{
                'Component' = $_.name
                'ID' = $_.id
                'Path' = ($xml | select-packageXmlNode "//dts:Property[ @dts:Name='BIPSOriginalFilePath' ]" | select -exp '#text' )
            }            
        }
    }

<# 
   .SYNOPSIS 
    Isolates components in the package that are missing input columns
   .DESCRIPTION
    Isolates components in the package that are missing input columns.

    These components are defined as those having child <inputs><input /></inputs>
    elements without a contained <inputColumns> element.
   .EXAMPLE 
    ls *.dtsx | find-componentMissingInputColumn
    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/22/2014 14:34:22 
#> 
}

function select-packageXmlNode
{
    [outputType( [System.Xml.XmlNode] )]
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [xml]
        # the package XML document
        $xml,
 
        [Parameter(Mandatory=$true, Position=1)]
        # the xpath to locate 
        $xpath
    )

    $nsm = new-object System.Xml.XmlNamespaceManager $xml.NameTable;
    $nsm.AddNamespace( 'dts',$dtsns );
        
    $xml.SelectNodes( $xpath, $nsm );

<# 
   .SYNOPSIS 
    Helper function for running xpath queries on package XML
   .DESCRIPTION
    Helper function for running xpath queries on package XML.

    Automatically associates the following prefixes and namespaces:
        dts => www.microsoft.com/SqlServer/Dts

   .EXAMPLE 
    ls *.dtsx | select-packageXmlNode "//dts:Property[ @dts:Name = 'PackageVariableValue' ]"
    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/22/2014 14:34:22 
#> 
}