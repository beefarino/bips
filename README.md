# Overview

BIPS is a PowerShell module for SSIS package management.  The goal of this project is to bring a devops workflow to working with SSIS packages.

## How You Can Help

At the moment, the biggest help would be for you to describe specific use cases as issues on this project.

# Building and Installing

## Prerequisites

To build the BIPS module, you will need the following:

1. The psake module (https://github.com/psake/psake)
2. .NET 4.5 SDK

## Running the Build and Install

To install the module from source, execute the following from a PowerShell prompt located at the root of the BIPS source tree:

```powershell
invoke-psake ./default.ps1 -task install
```

# Basic Use

Once installed, you pull the BIPS module in to your current PowerShell session using the standard import-module cmdlet:

```powershell
import-module bips
```

The BIPS module exposes a PSProvider that allows you to mount one or more SSIS packages as a PowerShell drive.  Once mounted, these packages can be navigated and manipulated as if they were folders and files on your hard drive.

## Mounting Packages

BIPS offers three ways to mount packages:

1. Mount a single local dtsx package file
2. Mount all dtsx files found in a local folder
3. Mount all packages deployed to a specified SQL server; this option will mount packages deployed to the database or the filesystem.

In all cases, you use the built-in new-psdrive cmdlet to mount the package(s):

```powershell
new-psdrive -name p -psprovider bips -root '<rootpath>'
```

The drive will contain different packages based on the value secified for <rootpath>, which must be one of the following:

1. The full path to a local dtsx file
2. The full path to a local folder containing one or more dtsx files
3. The name of the SQL Server instance to which to connect

For example, this command mounts all packages in the specified folder as the P drive:

```powershell
new-psdrive -name p -psprovider bips -root 'c:\myEtlProjects\'
```

## Navigating Packages

Once the drive is mounted, you can use the standard PowerShell provider cmdlets to move through the package object hierarchy.  In this example, the list of packages mounted on the P drive is listed:

```powershell
dir p:\packages
cd p:\packages\myEtlPackage\executables
dir | get-expression | where { $_.Property.Name -eq 'Description' } | set-expression -value "'LAST UPDATE: ' + (DT_WSTR,32)GETDATE()";
```

## Custom BIPS Commands

To get a list of custom commands shipped with BIPS, use the following command:

```powershell
get-command -module bips
```
Custom commands include full documentation, which can be accessed using the get-help command.  In this sample, the complete help for the get-expression BIPS custom command is retrieved.

```powershell
get-help get-expression -full
```
