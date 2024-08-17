# Spottify2.0
 Dieses Projekt ist im Rahmen des Moduls "System Architektur" von Studenten der Fachhochschule Erfurt erstellt worden. 

 ## How to install
 ### Prequisits 
 - Terraform
 - .NET SDK
   - Maui
   - ASP.Net Web Api

 ### Install 
 Alle Docker-Container werden von Terraform aufgesetzt und gestartet.  
 ⚠️ **Achten Sie darauf das Sie sich immer im Unterverzeichnis "FinalProject" befinden!** ⚠️
1. Navigieren in den Ordner **FinalProject** 
    ```cd FinalProject```
2. Terraform ausführen: 
    ```
    terraform init 
    terraform apply
    ```

#### Noch zu tun:
1. Api Starten
```
dotnet publish -c Release -o ./Spotify_Api/bin/Release --source ./Spotify_Api
dotnet ./FinalProject/Spotify_Api/bin/Release/net8.0/Spotify_Api.dll
```

## Testen
http://localhost:5000/swagger/index.html
