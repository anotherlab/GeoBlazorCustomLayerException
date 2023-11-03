# GeoBlazorCustomLayerException
Exception when adding an object to a GraphicsLayer created in code, but only in WASM

## Steps to replicate the problems ##
1. Restore the nuget packages
2. Edit [Program.cs](https://github.com/anotherlab/GeoBlazorCustomLayerException/blob/main/WasmCreateGraphicsLayers/Program.cs#L13) and replace "INSERT YOUR KEY HERE" with your API key
3. Build and the run app
4. You should see a map. There will be a layerlist widget with 3 layers listed. The one labelled "Code Behind" is created in the code, the other two in the markup
5. Click the "Draw Route" button. You will see a route and stops drawn on the markup layers. 
6. The stops should be also be drawn on the "Code Behind" but are not and an exception message will display below the map
