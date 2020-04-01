# Windows Redirection

Create small, single file redirection executables, also called shims, to actual applications for execution on the command line.

## Building from source

To build from source use:

``` shell
dotnet build
```

To publish for release:

``` shell
dotnet publish -c Release -o Builds
```

## Syntax

redirect source-file target-file arguments

## Example use case

Create a redirection executable in the current directory using the source filename as the target.

``` shell
> redirect [path-to-git]\git.exe

git.exe -> [path-to-git]\git.exe
```

## Known limitations

### Command line events are not forwarded

The redirection executable does not forward command line events.

CoreCLR events used for debugging dotnet core applications are not forwarded through the redirection. A warning is generated when attempting to debug:

```shell
The target process exited without raising a CoreCLR started event. Ensure that the target process is configured to use .NET Core. This may be expected if the target process did not run on .NET Core.
```
