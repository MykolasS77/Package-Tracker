
# Package Tracker 

This is a package tracking web application made with React and EF Core. 


## Features

- Creating a new package and storing information into a database.
- All pacakges are displayed at the main page.
- Filtering packages based on status and tracking id. 
- Changing package status either from the main page or package details page and storing the time of change.
- Package details page shows sender's and recipient's name, adress, phone number, package status and history of status changes.
 


## Requirements 

**.NET:** https://dotnet.microsoft.com/en-us/download

**Node.js:** https://nodejs.org/en/download/


## Run Locally

Clone the project

```bash
git clone https://github.com/MykolasS77/Package-Tracker
```

Go to frontend directory

```bash
cd to <project-location>/reactapp2.client
```

Install dependencies

```bash
npm i
```

Go to backend directory

```bash
cd to <project-location>/ReactApp2.Server
```

Start the application

```bash
dotnet run
```

Go to url displayed in the terminal (You may get an "ERR_CONNECTION_REFUSED" if you are using Chrome. If so, try using a different browser): 

<img width="352" height="106" alt="image" src="https://github.com/user-attachments/assets/a0c7b946-e424-47dc-8a55-fb07b1924bb3" />

## Usage

This is the main page. Right now it is empty because there are no packages created yet. Press "Create a new package" at the navbar. 

<img width="678" height="225" alt="image" src="https://github.com/user-attachments/assets/3e1cbcd7-461c-469d-b684-4bfe46432f04" />


Enter the details, click "Submit" and confirm. 

<img width="633" height="870" alt="image" src="https://github.com/user-attachments/assets/b9458dc2-e19b-492b-a2ef-be7aabcc96cc" />


Click on "View all packages". The package is now displayed at the main page. After adding more packages, click on "Filter based on status" to display only the packages
with desired status or find a specific package based on tracking number by using the search window. Click on "View" button to see package details.

<img width="843" height="255" alt="image" src="https://github.com/user-attachments/assets/cc2eb5ba-8e79-403a-8efc-3de5e9a9e4ab" />



This is the package details page. Change the status by clicking on "Created" and choose "Sent". If the status is "Canceled" or "Accepted", you won't be able to change it. 

<img width="595" height="708" alt="image" src="https://github.com/user-attachments/assets/fc944434-94dc-4d19-aaf2-d2d2ea4c7f45" />



Package status is now changed and package history table is updated.

<img width="595" height="727" alt="image" src="https://github.com/user-attachments/assets/c55c601e-4508-4291-9a49-7a5f043398b5" />













