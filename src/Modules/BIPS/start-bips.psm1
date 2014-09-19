function convertto-packageXml
{
    param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [Microsoft.SqlServer.Dts.Runtime.Package]
        # the package to convert to XML
        $package
    );

    process {
        [string] $xmlString;
        $package.SaveToXml( [ref]$xmlString, $null );

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
        
    }

<# 
   .SYNOPSIS 
    not implemented
   .DESCRIPTION
    not implemented
   .EXAMPLE 
    get-item myServer:/packages/myPackage | deploy-package

    Converts the package object found at the BIPS drive location to it's XML equivalent
   .NOTES
    AUTHOR: beefarino
    LASTEDIT: 09/19/2014 14:08:57 
#> 
}