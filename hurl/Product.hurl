
### List all
GET http://localhost:5269/api/Products
Accept: application/json

### Create
POST http://localhost:5269/api/Products
Accept: application/json

{
  "name": "FooBar",
  "price": 10.0
}

### Get By Id
GET http://localhost:5269/api/Products/1
Accept: application/json

### Not Found 404
GET http://localhost:5269/api/Products/111
Accept: application/json

### Not valid value 400
GET http://localhost:5269/api/Products/aaa
Accept: application/json

### delete 
DELETE http://localhost:5269/api/Products/10
Accept: application/json
