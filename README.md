# José's Bar
An API to a bar that manages the products replenishment and withdrawal.

## Doc
The documentation is available at https://localhost:7014/swagger/index.html when the server is started.

## Endpoints

| Path | Description |
| :--- | :--- |
| GET v1/products | get all products |
| GET v1/products/{id} | get product by id |
| POST v1/products | create product |
| PUT v1/products/{id} | update product by id|
| DELETE v1/products/{id} | delete product by id |
| GET v1/products/filters?descriprion={description}| filter product by description |


## How to test?

- clone the project;
- open it with Visual Studio;
- build the project;
- now you can either test in the swagger interface or through postman/insomnia with the url https://localhost:7014/v1
