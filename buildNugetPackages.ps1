rm *.nupkg
nuget pack .\BulletCircus.nuspec -IncludeReferencedProjects -Prop Configuration=Release
cp *.nupkg C:\Projects\Nugets\