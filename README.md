# OlimpiaIt.FacturaElectronica
Prueba técnica para la empresa OlimpiaIt.

# Ejercicio 1: 
# App en consola que dice si un número es multiplo de 3.

# Ejercicio 2: 
# App de tipo API Web que recibe la entidad Factura y realiza las valiaciones requeridas


## API - Metodo ValidarCampos
# http://localhost:55473/api/factura/ValidarCampos

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

## API - Metodo CalcularIVA
# http://localhost:55473/api/factura/CalcularIVA

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
    "Message": null,
    "JWTToken": null,
    "Data": 2375.00
}
`

## API - Metodo RecibirFacturas
# http://localhost:55473/api/factura/RecibirFacturas

#Request de ejemplo exitoso
`
[
    {
        "Id": 1,
        "Nit": 80012345,
        "Descripcion": "desc1",
        "ValorTotal": 12500,
        "PorcentIva": 19
    },
    {
        "Id": 2,
        "Nit": 9002222,
        "Descripcion": "desc2",
        "ValorTotal": 20500,
        "PorcentIva": 21.1
    },
    {
        "Id": 3,
        "Nit": 90033333,
        "Descripcion": "desc3",
        "ValorTotal": 3000,
        "PorcentIva": 3.31
    }
]
`

#Respuesta Ok
`
{
    "StatusCode": 200,
    "Message": "Facturas cargadas exitosamente. La suma de los ValoresTotale se encuentra en el campo Data",
    "JWTToken": null,
    "Data": 36000.0
}
`

#Respuesta con validaciones 1: cuando se repite el número de factura
`
{
    "StatusCode": 400,
    "Message": "Existen número de Facturas repetidas, No se puede procesar el cargue!",
    "JWTToken": null,
    "Data": -1.0
}
`

#Respuesta con validaciones 2: ejemplo campo %iva superior a 100
`
{
    "StatusCode": 202,
    "Message": "Algunas de las Facturas cargadas no paso las validaciones.",
    "JWTToken": null,
    "Data": [
        "La Factura 1, paso todas las validaciones exitosamente!",
        "La Factura 2, paso todas las validaciones exitosamente!",
        "La Factura 3, no cumplio las siguientes validaciones: El Porcentaje del Iva debe ser un valor entre 0 y 100"
    ]
}
`

