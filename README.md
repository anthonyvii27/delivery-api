# Delivery API

Welcome to the Delivery API! This API is a solution for managing products and sales in your application. Built with .NET, it provides a RESTful interface for creating and managing products and processing sales transactions.
This document provides an overview of the Delivery API endpoints, including detailed descriptions, request parameters, and response formats. The API includes endpoints for health checks, managing products, and handling sales.

---

## How to Run

1. Clone the repository:
   ```shell
   git clone https://github.com/anthonyvii27/delivery-api
   ```

2. Change to the repository directory:
   ```shell
   cd delivery-api
   ```
   
3. Run the command to start the containerized environment:
   ```shell
   make compose-up-v2
   ```

4. The API will be running on port `:8080` on your computer.
5. Optionally, in the root of this repository, there is a file named `delivery-api.http` that provides an easy-to-use interface for executing the implemented endpoints.

---

## Endpoints

### Health Check

#### `GET /health`

**Summary**: Checks the health status of the API.

**Responses**:

- **200 OK**
  ```json
  "Database connection is healthy."
  ```

- **503 Service Unavailable**
  ```json
  "Database connection isn't available."
  ```

- **500 Internal Server Error**
  ```json
  "An error occurred while checking the database connection."
  ```

---

### Products

#### `GET /products`

**Summary**: Retrieves all products.

**Responses**:

- **200 OK**
  ```json
  [
    {
      "id": 1,
      "name": "Product Name",
      "unitOfMeasurement": "UN",
      "price": 9.99
    }
  ]
  ```

---

#### `GET /products/{id}`

**Summary**: Retrieves a product by its ID.

**Parameters**:

- **Path**:
    - `id` (integer): The ID of the product to retrieve.

**Responses**:

- **200 OK**
  ```json
   {
     "id": 1,
     "name": "Product Name",
     "unitOfMeasurement": "UN",
     "price": 9.99
   }
  ```

- **404 Not Found**
  ```json
  {
    "message": "Product with ID {id} not found."
  }
  ```

---

#### `POST /products`

**Summary**: Creates a new product.

**Request Body**:
```json
{
  "name": "Product Name",
  "unitOfMeasurement": "Unit",
  "price": 9.99
}
```

**Responses**:

- **201 Created**
  ```json
  {
    "id": 1,
    "name": "Product Name",
    "unitOfMeasurement": "Unit",
    "price": 9.99
  }
  ```

- **400 Bad Request**
  ```json
  {
    "message": "Validation error messages"
  }
  ```

---

#### `PUT /products/{id}`

**Summary**: Updates an existing product.

**Parameters**:

- **Path**:
    - `id` (integer): The ID of the product to update.

**Request Body**:
```json
{
  "name": "Updated Product Name",
  "unitOfMeasurement": "Updated Unit",
  "price": 9.99
}
```

**Responses**:

- **200 OK**
  ```json
  {
    "id": 1,
    "name": "Updated Product Name",
    "unitOfMeasurement": "Updated Unit",
    "price": 9.99
  }
  ```

- **400 Bad Request**
  ```json
  {
    "message": "Validation error messages"
  }
  ```

- **404 Not Found**
  ```json
  {
    "message": "Product with ID {id} not found."
  }
  ```

---

#### `DELETE /products/{id}`

**Summary**: Deletes a product by its ID.

**Parameters**:

- **Path**:
    - `id` (integer): The ID of the product to delete.

**Responses**:

- **204 No Content**

- **400 Bad Request**
  ```json
  {
    "message": "Error message"
  }
  ```

- **404 Not Found**
  ```json
  {
    "message": "Product with ID {id} not found."
  }
  ```

- **409 Conflict**
  ```json
  {
    "message": "Cannot delete the product as it has associated sale items."
  }
  ```

---

### Sales

#### `GET /sales`

**Summary**: Retrieves all sales (historic).

**Responses**:

- **200 OK**
  ```json
  [
    {
      "id": 1,
      "saleDate": "2024-08-14T00:00:00Z",
      "totalAmount": 100.00,
      "saleItems": [
        {
          "id": 1,
          "productId": 1,
          "quantity": 2,
          "unitPrice": 50.00
        }
      ]
    }
  ]
  ```

---

#### `GET /sales/{id}`

**Summary**: Retrieves a sale by its ID.

**Parameters**:

- **Path**:
    - `id` (integer): The ID of the sale to retrieve.

**Responses**:

- **200 OK**
  ```json
  {
    "id": 1,
    "saleDate": "2024-08-14T00:00:00Z",
    "totalAmount": 100.00,
    "saleItems": [
      {
        "id": 1,
        "productId": 1,
        "quantity": 2,
        "unitPrice": 50.00
      }
    ]
  }
  ```

- **404 Not Found**
  ```json
  {
    "message": "Sale with ID {id} not found."
  }
  ```

---

#### `POST /sales`

**Summary**: Creates a new sale.

**Request Body**:
```json
{
  "saleDate": "2024-08-14T00:00:00Z",
  "zipCode": "12345678",
  "saleItems": [
    {
      "productId": 1,
      "quantity": 2
    }
  ]
}
```

**Responses**:

- **201 Created**
  ```json
  {
    "id": 1,
    "saleDate": "2024-08-14T00:00:00Z",
    "totalAmount": 100.00,
    "saleItems": [
      {
        "id": 1,
        "productId": 1,
        "quantity": 2,
        "unitPrice": 50.00,
        "product": {
          "id": 1,
          "name": "Product Name",
          "unitOfMeasurement": "Unit",
          "price": 50.00
        }
      }
    ]
  }
  ```

- **400 Bad Request**
  ```json
  {
    "message": "Validation error messages"
  }
  ```

---

#### `DELETE /sales/{id}`

**Summary**: Deletes (cancel) a sale by its ID.

**Parameters**:

- **Path**:
    - `id` (integer): The ID of the sale to delete.

**Responses**:

- **204 No Content**

- **400 Bad Request**
  ```json
  {
    "message": "Error message"
  }
  ```

- **404 Not Found**
  ```json
  {
    "message": "Sale with ID {id} not found."
  }
  ```