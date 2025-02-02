For node js apps:
  Install and run apps:
  1. install node modules: npm install
  2. run apps: node src/server.js

Publish the message using node js:
  post url: http://localhost:3000/passenger/publish
  request body:
  {
    "id": "123",
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "phone": "1234567890",
    "identificationType": "Passport",
    "identificationNumber": "A123456789",
    "identificationDocuments": ["passport.pdf", "visa.pdf"],
    "presentAddress": "123 Main St, City, Country",
    "permanentAddress": "456 Elm St, City, Country",
    "status": "active",
    "rating": 4.5,
    "referenceId": "ref12345"
  }

Publish the message using dotnet:
  post url: http://localhost:5000/passenger
  request body:
  {
  "id": "123",
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phone": "1234567890",
  "identificationType": "Passport",
  "identificationNumber": "A123456789",
  "identificationDocuments": ["passport.pdf", "visa.pdf"],
  "presentAddress": "123 Main St, City, Country",
  "permanentAddress": "456 Elm St, City, Country",
  "status": "active",
  "rating": 4.5,
  "referenceId": "ref12345",
  "createdAt": 1699000000000,
  "updatedAt": 1699000000000
}

Get the messages:
 get url: http://localhost:5000/Passenger
