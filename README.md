# Spottify2.0
 Dieses Projekt ist im Rahmen des Moduls "System Architektur" von Studenten der Fachhochschule Erfurt erstellt worden. 

 ## How to install
 ### Prequisits 
 - Terraform
 - .NET SDK
   - -NET Maui
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

### How to run (Mac)
```cd Spottify2```
```dotnet build -t:Run -f net8.0-maccatalyst```



## Api separat Testen
http://localhost:85/swagger/index.html