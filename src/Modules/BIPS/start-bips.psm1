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

function get-package
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [Alias("pspath")]
        [string]
        # a path to a BIPS drive element
        $path
    );

    process {
        
        $path | get-packagePath | get-item;
    }
<# 
   .SYNOPSIS 
    Resolves the package for the specified BIPS drive element.
   .DESCRIPTION
    Resolves the package for the specified BIPS drive element.
   .EXAMPLE 
    ls -rec | where hasExpression | select PSChildName, PSPath, @{n='Package';e={$_|get-package|select -exp name}}

    outputs the list of items that use expressions, and their associated packages
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 10/01/2014 15:24:36
#> 
}

function get-packagePath
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [Alias("pspath")]
        [string]
        # a path to a BIPS drive element
        $path
    );

    process {
        
        if( $path -notmatch 'packages\\([^\\]+)\\.+' )
        {
            return;
        }

        $packagePath = $path -replace 'packages\\([^\\]+)\\.+', 'packages\$1';
        $packagePath;
    }
<# 
   .SYNOPSIS 
    Resolves the path to the package for the specified BIPS drive element.
   .DESCRIPTION
    Resolves the path to the package for the specified BIPS drive element.
   .EXAMPLE 
    ls -rec | where hasExpression | select PSChildName, PSPath, @{n='PackagePath';e={$_|get-packagePath}}

    outputs the list of items that use expressions, and their associated package paths
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 10/01/2014 15:24:36
#> 
}

function get-component
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [Alias("pspath")]
        [string]
        # a path to a BIPS drive element
        $path
    );

    process {
        
        if( $path -notmatch 'packages\\([^\\]+)\\executables\\([^\\]+)' )
        {
            return;
        }

        $componentPath = $path -replace 'packages\\([^\\]+)\\executables\\([^\\]+)\\.+', 'packages\$1\executables\$2';
        $componentPath | get-item;
    }
<# 
   .SYNOPSIS 
    Resolves the main component containing the specified BIPS drive element.
   .DESCRIPTION
    Resolves the main component containing the specified BIPS drive element.
   .EXAMPLE 
    ls -rec | where hasExpression | select PSChildName, PSPath, @{n='Component';e={$_|get-component|select -exp name}}

    outputs the list of items that use expressions, and their associated packages
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 10/01/2014 15:24:36
#> 
}

function get-connectionManager
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [Alias("pspath")]
        [string]
        # a path to a BIPS drive element
        $path,

        [Parameter(ValueFromPipelineByPropertyName=$true)]
        [string]
        # the name of the connection manager to retrieve
        $connection,

        [Parameter(ValueFromPipelineByPropertyName=$true)]
        [string]
        # the connection manager id to retrieve
        $connectionmanagerid,

        [Parameter(ValueFromPipelineByPropertyName=$true)]
        [string]
        # the name of the destination connection manager to retrieve
        $destinationconnection,

        [Parameter(ValueFromPipelineByPropertyName=$true)]
        [string]
        # the name of the source connection manager to retrieve
        $sourceconnection
    );

    process {
        if( -not( $connectionmanagerid -or $connection ) )
        {
            return;
        }

        $path | get-packagePath | join-path -child connections | get-childItem | where {
            ( $connectionmanagerid -and ( $_.ID -eq  $connectionmanagerid ) ) -or ( $connection -and ( $_.name -eq  $connection ) ) 
        };
    }
<# 
   .SYNOPSIS 
    Retrieves the connection manager for the input object.
   .DESCRIPTION
    Retrieves the connection manager for the input object.
   .EXAMPLE 
    get-item . | get-connectionManager

    outputs the connection manager associated with the specified object
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 10/01/2014 15:24:36
#> 
}

function get-expression
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [Alias("pspath")]
        [string]
        # a path to a BIPS drive element
        $path,

        [Parameter()]
        [switch]
        # when specified, all expressions child nodes under the specified path are output
        $recurse,

        [Parameter()]
        [switch]
        # when specified, all properties, including those with no expressions, are included in the output
        $force
    );

    process {
        $items = @(get-item $path) | where hasExpressions;
        if( $recurse ) {
            $items += dir $path -recurse | where hasExpressions;
        }

        $items | foreach {
            $item = $_;
            $component = $item | get-component;
            $package = $null;
            $props = $item | select -ExpandProperty properties;
            
            $props | foreach {
                $prop = $_;

                $expr = $_.getExpression($item);
                if( $force -or $expr )
                {
                    if( -not $package )
                    {
                        $package = $item | get-package;
                    }

                    $o =  new-object  psobject -prop @{
                        Component = $component;
                        Package = $package;
                        Item = $item;
                        Property = $prop;
                        Expression = $expr;
                    };
                    $o.PSTypeNames.Clear();
                    $o.PSTypeNames.Add( "CodeOwls.BIPS.ExpressionDescriptor" );

                    $o;
                }
            }
        }
        
    }
<# 
   .SYNOPSIS 
    Outputs all expressions employed by the BIPS object at the specified path.
   .DESCRIPTION
    Outputs all expressions employed by the BIPS object at the specified path.

    When -recurse is specified, objects contained by the specified path are also output.
   .EXAMPLE 
    get-expression . 

    outputs the expressions employed by the object at the current BIPS path
   .EXAMPLE 
    get-expression . -recurse

    outputs the expressions employed by all objects at or under the current BIPS path
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 10/01/2014 15:24:36
#> 
}

function set-expression
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        # the expression property to set, as retrieved by get-expression
        $expression,

        [Parameter(Mandatory=$true)]
        [string]
        # when specified, all expressions child nodes under the specified path are output
        $value
    );

    process {
        $expr = $expression.expression;
        $item = $expression.item;
        $prop = $expression.property;
        $prop.setExpression($item, $value);        
    }
<# 
   .SYNOPSIS 
    Sets the expression for the specified property.
   .DESCRIPTION
    Sets the expression for the specified property.    
   .EXAMPLE 
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 10/01/2014 15:24:36
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

function reset-designTimeLayout
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [CodeOwls.Bips.Utility.XmlFile()]
        [xml]
        # the package XML to auto-layout
        $xml
    );

    process {

        $version = $xml | select-packageXmlNode "/dts:Executable/@dts:ExecutableType";

        switch -Regex ( $version.Value )
        {
            '2$' { 
                $xml | select-packageXmlNode ("//dts:PackageVariable[ ./dts:Property[ @dts:Name='Namespace' and ./text()='dts-designer-1.0' ] ] | " +
                        "//dts:Variable[ ./dts:Property[ @dts.Name='ObjectName' and ( ./text()='varSSISOps_PkgLayout' or ./text()='varSSISOps_DfLayout' ) ]")| foreach {
                    $_.parentNode.removeChild( $_ ) | out-null;                        
                }
                break;
            }

            '3$' {
                $xml | select-packageXmlNode "/dts:Executable/dts:DesignTimeProperties" | foreach {
                    
                    $dtxml = [xml]$_.'#cdata-section';
                    $dtxml | select-packageXmlNode "//graph:NodeLayout | //graph:EdgeLayout" | foreach {
                        $_.parentNode.removeChild( $_ ) | out-null;
                    }
                    
                    $xmlstr = $dtxml.OuterXml;
                    $_.'#cdata-section' = $xmlstr;
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
    ls *.dtsx | reset-designTimeLayout

    Removes all DTS layout information from the packages in the current folder
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


function save-package
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true, ParameterSetName='xml')]
        [xml]
        # the package XML to save
        $xml,

        [Parameter(Mandatory=$true, ValueFromPipeline=$true, ParameterSetName='package')]
        [CodeOwls.BIPS.Utility.PackageDescriptor]
        # the package to save
        $package,

        [Parameter(ParameterSetName='package')]
        [Parameter(ParameterSetName='xml')]
        [alias("destination")]
        [string]
        # the file or folder location to output the package XML; if unspecified, defaults to the current filesystem location
        $outputPath = (Get-Location -PSProvider filesystem),

        [Parameter(ParameterSetName='package')]
        [switch]
        $passthru
    );
    
    process {
        if( $package )
        {
            $originalFilePath = $package.Location;
        }
        else
        {
            $originalFilePathNode = $xml | select-packageXmlNode "//dts:Property[ @dts:Name='BIPSOriginalFilePath' ]";
            $originalFilePath = $originalFilePathNode.'#text';
            $originalFilePathNode.ParentNode.RemoveChild($originalFilePathNode) | out-null;
            write-debug "Original BIPS file path: $originalFilePath"
        }
        
        $path =  $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($outputPath);
        if( -not [io.file]::exists( $path ) )
        {
            $filename = (split-path $originalFilePath -Leaf)
            $path = $path | join-path -child $filename
        }

        write-debug "Saving package to file path: $path"
        if( $package )
        {            
            $ssisApplication.SaveToXml( $path, $package, $null );
            if($passthru)
            {
                $package;
            }
        }
        else
        {
            $xml.Save( $path );
            $xml;
        }        
    }

<# 
   .SYNOPSIS 
    saves the specified package to the local file system
   .DESCRIPTION
    saves the specified package to the local file system
   .EXAMPLE 
    ls *.dtsx | disable-validation | save-pacakge
    
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
    $nsm.AddNamespace( 'graph', "clr-namespace:Microsoft.SqlServer.IntegrationServices.Designer.Model.Serialization;assembly=Microsoft.SqlServer.IntegrationServices.Graph" );
        
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

@(
    'Microsoft.SqlServer.Dts.Runtime.Application', 
    'Microsoft.SqlServer.Dts.Runtime.ConnectionManagerBase',
    'Microsoft.SqlServer.Dts.Runtime.ConnectionManager',
    'Microsoft.SqlServer.Dts.Runtime.HttpClientConnection',
    'Microsoft.SqlServer.Dts.Runtime.ConnectionManagerItem',
    'Microsoft.SqlServer.Dts.Runtime.ManagedWrapper',
    'Microsoft.SqlServer.Dts.Runtime.Package',
    'Microsoft.SqlServer.Dts.Runtime.Project',
    'Microsoft.SqlServer.Dts.Runtime.DtsProperty',
    'Microsoft.SqlServer.Dts.Runtime.DtsConnectionAttribute',
    'Microsoft.SqlServer.Dts.Runtime.ConnectionInfo',
    'Microsoft.SqlServer.Dts.Runtime.PackageUpgradeOptions',
    'Microsoft.SqlServer.Dts.Runtime.Localized',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSPackage100',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSProperty100',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.PackageClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.PackageNeutralClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.PackageRemote32Class',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.PackageRemote64Class',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSConnectionManager100',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerHostClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSConnectionManagerDatabaseParameters100',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerOleDbClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerOLAPClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerOdbcClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerAdoClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerAdoNetClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerSqlMobileClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerFileClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerMultiFileClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerFlatFileClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerMultiFlatFileClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSApplication100',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ApplicationClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSConnectionManagerHttp100',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerHttpClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSHttpClientConnection100',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.HttpClientConnection100Class',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerFtpClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerExcelClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerCacheClass',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSConnectionInfo100',
    'Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSManagedWrapper100',
    'Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask.ExecuteSQLTask',
    'Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100',
    'Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSRuntimeConnection100',
    
    'Microsoft.SqlServer.Dts.Tasks.TransferDatabaseTask.TransferDatabaseTask',
    'Microsoft.SqlServer.Dts.Tasks.BulkInsertTask.BulkInsertTask',
    'Microsoft.SqlServer.Dts.Tasks.TransferErrorMessagesTask.TransferErrorMessagesTask',
    'Microsoft.SqlServer.Dts.Tasks.TransferJobsTask.TransferJobsTask',
    'Microsoft.SqlServer.Dts.Tasks.TransferLoginsTask.TransferLoginsTask',
    'Microsoft.SqlServer.Dts.Tasks.TransferSqlServerObjectsTask.TransferSqlServerObjectsTask',
    'Microsoft.SqlServer.Dts.Tasks.TransferStoredProceduresTask.TransferStoredProceduresTask',

    'CodeOwls.BIPS.BipsProxyIDTSComponentMetaData100',
    'CodeOwls.BIPS.BipsProxyIDTSRuntimeConnection100',
    'CodeOwls.BIPS.Utility.PackageDescriptor' 
 ) | Update-TypeData -MemberType ScriptProperty -MemberName 'ConnectionManager' -value {
    $this | get-connectionManager     
};

@(
    'Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask.ExecuteSQLTask',   
    'Microsoft.SqlServer.Dts.Tasks.TransferDatabaseTask.TransferDatabaseTask',
    'Microsoft.SqlServer.Dts.Tasks.BulkInsertTask.BulkInsertTask',
    'Microsoft.SqlServer.Dts.Tasks.TransferErrorMessagesTask.TransferErrorMessagesTask',
    'Microsoft.SqlServer.Dts.Tasks.TransferJobsTask.TransferJobsTask',
    'Microsoft.SqlServer.Dts.Tasks.TransferLoginsTask.TransferLoginsTask',
    'Microsoft.SqlServer.Dts.Tasks.TransferSqlServerObjectsTask.TransferSqlServerObjectsTask',
    'Microsoft.SqlServer.Dts.Tasks.TransferStoredProceduresTask.TransferStoredProceduresTask',

    'CodeOwls.BIPS.BipsProxyIDTSCustomProperty100'
 ) | Update-TypeData -MemberType ScriptProperty -MemberName 'Sql' -value {
    if($this.name -eq 'OpenRowset') { $this.value }
    else { $this.sqlstatementsource }
};
