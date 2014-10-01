param( 
    [parameter(valuefrompipeline = $true, mandatory=$true)]
    [type]
    $type 
)

process {
	sleep -seconds 3;
	write-progress -activity 'new-interfacewrapper' -currentOperation $type.Name
    $t = $type
    $className = 'BipsProxy' + $t.name 
	$typeName = $t.name;

    new-item -name $className -type class -access public | out-null

	$methods = $t.getMethods() | where IsSpecialName -eq $false;
	$properties = $t.getProperties();
	$code = @();
	$code += $methods | foreach { 
        $f = $_; 
		$r = $f.returnType
		$p = ($f.getParameters() | foreach { if( $_.parametertype.Name -match "^.+&" ) { 'ref ' + $_.Name } else { $_.Name } } ) -join ',';
		$pd = ($f.getParameters() | foreach { "" + ( $_.parametertype.Name -replace "^(.+)&$",'ref $1' ) + " " + $_.Name }) -join ',';
		$m = $f.Name;
        $return = if( $r -match 'void' ) { '' } else { 'return ' }

        "public $r $m($pd) { $return _innerObject.$m($p); }"
    }
	
	$code += $properties | foreach {
		$f = $_;
		$canget = $_.canread;
		$canset = $_.canwrite;
		$r = $f.propertyType;
		$p = $_.Name;
		$get = '';
		$set = '';
		$indexer = '';
		$indexerd = '';
		$ips = $f.getIndexParameters();

		if( $ips  )
		{
			write-warning "indexed property $p cannot be implemented";
			return;
		}

		if( $canget )
		{
			$get = "get { return _innerObject.$p$indexer; }";
		}
		if( $canset )
		{
			$set = "set { _innerObject.$p$indexer = value; } ";
		}

		"public $r $p$indexerd { $get $set }";
	}
	
	set-content ./$className -value @"
	public class $className 
	{
		private readonly $typeName _innerObject;

		internal $className( $typeName innerObject )
		{
			_innerObject = innerObject;
		}

		$( $code -join "`r`n" )
	}
"@
	
}

end {
	write-progress -activity 'new-interfacewrapper' -complete
}







