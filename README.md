# OlimpiaIt.FacturaElectronica
Prueba técnica para la empresa OlimpiaIt.

# Ejercicio 1: App en consola que dice si un número es multiplo de 3
# Ejercicio 2: App de tipo API Web que recibe la entidad Factura y realiza las valiaciones requeridas


## API - Metodo ValidarCampos
## http://localhost:55473/api/factura/ValidarCampos

#Request de ejemplo exitoso
`
{
    "Id": 1,
    "Nit": 80012345,
    "Descripcion": "desc1",
    "ValorTotal": 12500,
    "PorcentIva": 19
}
`

#Respuesta Ok
`
{
    "StatusCode": 200,
    "Message": "La Factura 1, paso todas las validaciones exitosamente!",
    "JWTToken": null,
    "Data": 12500.0
}
`

#Respuesta con validación
`
{
    "StatusCode": 400,
    "Message": "La Factura 1, no cumplio las siguientes validaciones: El Porcentaje del Iva debe ser un valor entre 0 y 100",
    "JWTToken": null,
    "Data": 0.0
}
`