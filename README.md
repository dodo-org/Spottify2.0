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

### Todo
- Container Api erst nach postgres starten

## Testen
http://localhost:5000/swagger/index.html
