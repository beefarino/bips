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
        [string] $xmlString;
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

function deploy-package
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [CodeOwls.BIPS.Utility.PackageDescriptor]
        # the package to save
        $package
    );

    process {
        throw "not implemented";
    }

<# 
   .SYNOPSIS 
    not implemented
   .DESCRIPTION
    not implemented
   .EXAMPLE 
    get-item myServer:/packages/myPackage | deploy-package

    
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
        $pkg = $package.Package;
        $path = $package.Location;
    }

<# 
   .SYNOPSIS 
    not implemented
   .DESCRIPTION
    not implemented
   .EXAMPLE 
    get-item myServer:/packages/myPackage | deploy-package

    
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 14:08:57 
#> 
}

function clear-packageLayout
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [Microsoft.SqlServer.Dts.Runtime.Package]
        # the package to auto-layout
        $package,

        [Parameter()]
        [switch]
        # when specified, drops the package back on the pipeline
        $passThru
    );

    process {
        $package.ExtendedProperties | where {
            $_.namespace -match 'dts-designer'
        } | foreach{
            $package.ExtendedProperties.Remove( $_.ID )
        }

        if( $passThru )
        {
            $package;
        }
    }

<# 
   .SYNOPSIS 
    removes all DTS layout formatting from the package
   .DESCRIPTION
    removes all DTS layout formatting from the package
   .EXAMPLE 
    get-item myServer:/packages/myPackage | clear-packageLayout

    Removes all DTS layout information from the package at the specified BIPS location
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 17:28:44 
#> 
}