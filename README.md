# Crazy Musicians API

This is a simple ASP.NET Core Web API project that manages a list of **Crazy Musicians** with basic CRUD operations. Each musician has an ID, Name, Profession, and a Fun Feature. The API supports various endpoints, including GET, POST, PATCH, PUT, and DELETE, and uses model validation to ensure data integrity.

## Table of Contents

- [Technologies Used](#technologies-used)
- [API Endpoints](#api-endpoints)
  - [Get All Musicians](#get-all-musicians)
  - [Get Musician by ID](#get-musician-by-id)
  - [Create a New Musician](#create-a-new-musician)
  - [Update Musician Profession (Patch)](#update-musician-profession-patch)
  - [Delete Musician](#delete-musician)
  - [Search Musicians by Profession](#search-musicians-by-profession)
- [Model Validation](#model-validation)
- [Installation and Running the Project](#installation-and-running-the-project)
- [Sample Data](#sample-data)

## Technologies Used

- ASP.NET Core Web API
- C#
- Newtonsoft.Json (for JSON Patch support)
- Swagger (for API documentation)

## API Endpoints

### Get All Musicians

```
GET /api/crazymusicians
```

This endpoint returns a list of all the crazy musicians.

### Get Musician by ID

```
GET /api/crazymusicians/{id:int:min(1)}
```

Returns a single musician by their ID. If the musician does not exist, returns a `404 Not Found`.

### Create a New Musician

```
POST /api/crazymusicians
```

- Body:
  ```json
  {
    "name": "John Doe",
    "profession": "Crazy Guitarist",
    "funFeature": "Plays guitar with his feet"
  }
  ```

- Validations:
  - `name`, `profession`, and `funFeature` are required fields.
  
If any of these fields are missing, a `400 Bad Request` will be returned along with the validation errors.

### Update Musician Profession (Patch)

```
PATCH /api/crazymusicians/reprofession/{id:int:min(1)}/{newProfession}
```

This endpoint allows updating the profession of a musician. It requires both the musician's ID and the new profession as URL parameters.

- URL Parameters:
  - `id`: Musician ID (must be an integer greater than 1)
  - `newProfession`: The new profession for the musician

- Body (Example):
  ```json
  [
    { "op": "replace", "path": "/funFeature", "value": "Now sings opera while playing" }
  ]
  ```

- Validations:
  - `newProfession` cannot be empty or null.
  - The `JsonPatchDocument` must be valid, or it will return a `400 Bad Request`.

### Delete Musician

```
DELETE /api/crazymusicians/{id:int:min(1)}
```

Deletes the musician with the specified ID. If the musician does not exist, returns `404 Not Found`.

### Search Musicians by Profession

```
GET /api/crazymusicians/search?profession={profession}
```

Allows searching for musicians by profession using the `FromQuery` parameter. This will return all musicians that match the given profession.

## Model Validation

Model validation ensures that all required fields are present in the request payloads. The following fields are required for the **CrazyMusician** model:

- `Name`: Required.
- `Profession`: Required.
- `FunFeature`: Required.

If any of these fields are missing or invalid during the POST or PATCH requests, the API will return a `400 Bad Request` with a validation error message.

Sample validation error response:

```json
{
  "name": [
    "The Name field is required."
  ],
  "profession": [
    "The Profession field is required."
  ],
  "funFeature": [
    "The FunFeature field is required."
  ]
}
```

### PATCH Validation:

- The `newProfession` in the PATCH request cannot be null or empty.
- The `JsonPatchDocument` must be valid; otherwise, a `400 Bad Request` with a detailed error message is returned.

## Installation and Running the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/sburakozdemir/crazy-musicians-api.git
   ```

2. Navigate to the project directory:
   ```bash
   cd crazy-musicians-api
   ```

3. Restore the dependencies:
   ```bash
   dotnet restore
   ```

4. Run the project:
   ```bash
   dotnet run
   ```

5. The API will be available at `https://localhost:5001` or `http://localhost:5000`. You can access the Swagger UI for testing the API at `https://localhost:5001/swagger`.

## Sample Data

The following sample musicians are pre-populated:

| ID  | Name           | Profession               | Fun Feature                                             |
| --- | -------------- | ------------------------ | ------------------------------------------------------- |
| 1   | Ahmet Çalgı    | Ünlü Çalgı Çalar          | Her zaman yanlış nota çalar, ama çok eğlenceli           |
| 2   | Zeynep Melodi  | Popüler Melodi Yazarı     | Şarkıları yanlış anlaşılır ama çok popüler               |
| 3   | Cemil Akor     | Çılgın Akorist            | Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli|
| 4   | Fatma Nota     | Sürpriz Nota Üreticisi    | Nota üretirken sürekli sürprizler hazırlar               |
| 5   | Hasan Ritim    | Ritim Canavarı            | Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir   |
| 6   | Elif Armoni    | Armoni Ustası             | Armonilerini bazen yanlış çalar, ama çok yaratıcıdır     |
| 7   | Ali Perde      | Perde Uygulayıcı          | Her perdeyi farklı şekilde çalar, her zaman sürprizlidir |
| 8   | Ayşe Rezonans  | Rezonans Uzmanı           | Rezonans konusunda uzman, ama bazen çok gürültü çıkarır  |
| 9   | Murat Ton      | Tonlama Meraklısı         | Tonlamalarındaki farklılıklar bazen komik, ama ilginç    |
| 10  | Selin Akor     | Akor Sihirbazı            | Akorları değiştirdiğinde bazen sihirli bir hava yaratır  |
