def buscar_archivos(sistema, ruta_objetivo):
    archivos_encontrados = []

    def recorrer(nodo, ruta_actual):
        ruta_completa = ruta_actual + "/" + nodo["name"] if nodo["name"] else ruta_actual

        if nodo["isFile"]:
            archivos_encontrados.append(ruta_completa)
        else:
            for hijo in nodo.get("children", []):
                recorrer(hijo, ruta_completa)

    def encontrar_carpeta(nodo, ruta_partes):
        if not ruta_partes:
            recorrer(nodo, "")  
            return
        for hijo in nodo.get("children", []):
            if hijo["name"] == ruta_partes[0] and not hijo["isFile"]:
                encontrar_carpeta(hijo, ruta_partes[1:])

    partes_ruta = [parte for parte in ruta_objetivo.strip("/").split("/") if parte]
    encontrar_carpeta(sistema, partes_ruta)
    return archivos_encontrados


# Ejemplo 
sistema = {
    "name": "",
    "isFile": False,
    "children": [
        {
            "name": "home",
            "isFile": False,
            "children": [
                {
                    "name": "user",
                    "isFile": False,
                    "children": [
                        {"name": "foto.jpg", "isFile": True},
                        {
                            "name": "documentos",
                            "isFile": False,
                            "children": [
                                {"name": "cv.pdf", "isFile": True},
                                {"name": "certificado.png", "isFile": True}
                            ]
                        }
                    ]
                }
            ]
        }
    ]
}

ruta = "/home/user"
resultado = buscar_archivos(sistema, ruta)

print("Archivos encontrados:")
for archivo in resultado:
    print(archivo)
