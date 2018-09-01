# SportStore

SportStore es un sistema de venta online para una tienda de artículos deportivos que cumple con los requisitos especificados en la [letra del trabajo obligatorio de la asignatura Diseño de Aplicaciones II](https://1drv.ms/b/s!AsbRSTL8VkzxgZ5bO326Uxuu2EqavQ) de la carrera Ingeniería de Sistemas de la Universidad ORT Uruguay.  

## API

| Method    | Endpoint                                          | Descripción                         | Estado
| --------- | ------------------------------------------------- | ----------------------------------- | ------
| POST      | /api/User/SignUp                                  | Registro de usuario                 | OK
| POST      | /api/User/Login                                   | Inicio de sesión                    | OK
| POST      | /api/User/Logout                                  | Cierre de sesión                    | OK
| GET       | /api/User                                         | Listar usuarios                     | OK
| GET       | /api/User/`username`                              | Detalle de usuario                  | OK
| POST      | /api/User                                         | Alta de usuario                     | OK
| PUT       | /api/User/`username`                              | Edición de usuario                  | OK
| DELETE    | /api/User/`username`                              | Baja de usuario                     | OK
| GET       | /api/Product                                      | Listar productos                    | OK
| GET       | /api/Product/`code`                               | Detalle de producto                 | OK
| POST      | /api/Product                                      | Alta de producto                    | OK
| PUT       | /api/Product/`code`                               | Edición de producto                 | OK
| DELETE    | /api/Product/`code`                               | Baja de producto                    | OK
| GET       | /api/Cart                                         | Listar productos en el carrito      | OK
| PUT       | /api/Cart                                         | Agregar un producto al carrito      | OK
| DELETE    | /api/Cart                                         | Quitar un producto del carrito      | OK
| POST      | /api/Cart/CheckOut                                | Efectuar compra                     | OK
| GET       | /api/Purchase/`id`                                | Detalle de una compra               | OK
| GET       | /api/Review/`code`                                | Listar reviews de producto          | OK
| GET       | /api/Review/`code`/`id`                           | Detalle de review de producto       | OK
| POST      | /api/Review/`code`                                | Alta de review de producto          | OK
| GET       | /api/Category                                     | Listar categorías                   | OK
| GET       | /api/Category/`id`                                | Detalle de categoría                | OK
| POST      | /api/Category                                     | Alta de categoría                   | OK
| PUT       | /api/Category/`id`                                | Edición de categoría                | OK
| DELETE    | /api/Category/`id`                                | Baja de categoría                   | OK
| GET       | /api/Manufacturer                                 | Listar fabricantes                  | OK
| GET       | /api/Manufacturer/`id`                            | Detalle de fabricante               | OK
| POST      | /api/Manufacturer                                 | Alta de fabricante                  | OK
| PUT       | /api/Manufacturer/`id`                            | Edición de fabricante               | OK
| DELETE    | /api/Manufacturer/`id`                            | Baja de fabricante                  | OK
| GET       | /api/PaymentMethod                                | Listar métodos de pago              | OK
| GET       | /api/PaymentMethod/`id`                           | Detalle de método de pago           | OK
| POST      | /api/PaymentMethod                                | Alta de método de pago              | OK
| PUT       | /api/PaymentMethod/`id`                           | Edición de método de pago           | OK
| DELETE    | /api/PaymentMethod/`id`                           | Baja de método de pago              | OK
| GET       | /api/Role                                         | Listar métodos de rol de usuario    | OK
| GET       | /api/Role/`id`                                    | Detalle de método de rol de usuario | OK
| POST      | /api/Role                                         | Alta de método de rol de usuario    | OK
| PUT       | /api/Role/`id`                                    | Edición de método de rol de usuario | OK
| DELETE    | /api/Role/`id`                                    | Baja de método de pago              | OK
| GET       | /api/ShippingAddress                              | Listar direcciones de entrega       | OK
| GET       | /api/ShippingAddress/`id`                         | Detalle de dirección de entrega     | OK
| POST      | /api/ShippingAddress                              | Alta de dirección de entrega        | OK
| PUT       | /api/ShippingAddress/`id`                         | Edición de dirección de entrega     | OK
| DELETE    | /api/ShippingAddress/`id`                         | Baja de dirección de entrega        | OK
| GET       | /api/Management/Report/PurchaseByCategory         | Listar productos                    | OK
| GET       | /api/Management/Report/PurchasedProductRanking    | Detalle de producto                 | OK
