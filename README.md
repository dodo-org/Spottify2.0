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

### How to run
```cd Spottify2```
```dotnet build -t:Run -f net8.0-maccatalyst```

### Todo
- Container Api erst nach postgres starten

## Testen
http://localhost:5000/swagger/index.html


  nginx-least-connections:
    image: nginx:latest
    ports:
      - "8082:80"
    volumes:
      - ./least_connections.custom.conf:/etc/nginx/conf.d/least_connections.custom.conf:ro
    command: /bin/sh -c "rm /etc/nginx/conf.d/default.conf && nginx -g 'daemon off;'"
    depends_on:
      - backend1
      - backend2
      - backend3