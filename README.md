# Nuget Source Manager

[![Build status](https://ci.appveyor.com/api/projects/status/4vubn0uckh5kdrtx?svg=true)](https://ci.appveyor.com/project/bilal-fazlani/nugetsourcemanager)

## What is it ?
It's a command line tool to manage nuget sources. With this you can easily add/update/remove/enable/disable and list the nuget sources in a Nuget.Config file.
This functionality is already present in nuget.exe but this is an easy to use implementation. Read more to know the difference.

## How do I add a nuget source ?
```
NugetSourceManager add "D:\LocalNugetSource" "MyLocalNugetPackages"
```
Or simply
```
NugetSourceManager add "D:\LocalNugetSource"
```
If you don't specify a name for source, random name will be assigned.

## How do I update a nuget source ?

It's simple. Just use the same command as above. If the source name exists, it will be updated.
```
NugetSourceManager add "D:\LocalNugetSource\NewPackages" "MyLocalNugetPackages"
```

## What if I want to update the name of source and not the source path ?
Again. Just use the same command as above.

```
NugetSourceManager add "D:\LocalNugetSource\NewPackages" "MyLocalNugetPackages_Latest"
```
It checks the Nuget.Config file for the above source path. If it exists, it will update it's name.

## Really ? wow. Well, what if the Nuget.Config doesn't exist ?
It will create one for you.

## Seems easy. But, can I specify the path of Nuget.Config file?
By default it uses `%APPDATA%\NuGet\NuGet.Config` but you specify the path as follows

```
NugetSourceManager add "D:\LocalNugetSource\NewPackages" "MyLocalNugetPackages_Latest" -ConfigFile "d:\Nuget.Config"
```

## Interesting. How do I remove a source?

For most of the operations, you can provide either the name or just the path. It will identify the source and perform the operation.
So removal can be done by
```
NugetSourceManager remove "D:\LocalNugetSource\NewPackages"
```
Or 
```
NugetSourceManager remove "MyLocalNugetPackages_Latest"
```

## Cool. Can I enable or disable a source ?
Yes. Similar to 'remove` command, it supports name or path.

```
NugetSourceManager enable "D:\LocalNugetSource\NewPackages"
```
Or
```
NugetSourceManager enable "D:\LocalNugetSource\NewPackages"
```
Or
```
NugetSourceManager disable "InternalNugetRepo"
```

## How to see list to of nuget sources ?

```
NugetSourceManager list
```


