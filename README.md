# SafeVault Security Hardening Project - Capstone

## 📋 Descripción General
SafeVault es una aplicación web diseñada para gestionar datos confidenciales de forma segura. Este proyecto integra las tres fases de endurecimiento de seguridad:
1. **Codificación Segura e Input Validation** (Prevención de XSS e Inyección SQL).
2. **Autenticación y Autorización** (Hashing de contraseñas con BCrypt y Control de Acceso Basado en Roles - RBAC).
3. **Depuración y Pruebas Unitarias** (Verificación y aseguramiento mediante NUnit).

---

## 🔍 Resumen de Vulnerabilidades Identificadas y Correcciones Aplicadas

* **Inyección SQL (SQLi):**
  * *Vulnerabilidad:* Concatenación directa de cadenas de texto en las consultas a la base de datos.
  * *Corrección:* Sustitución por **sentencias parametrizadas** en C# (`SqlCommand` con parámetros tipados), asegurando que los datos de entrada sean tratados como literales.
* **Secuencias de Comandos en Sitios Cruzados (XSS):**
  * *Vulnerabilidad:* Ausencia de sanitización y codificación en formularios de cara al usuario.
  * *Corrección:* Implementación de codificación de salida mediante `WebUtility.HtmlEncode()` y validación estricta de expresiones regulares.
* **Control de Acceso Inseguro / Autenticación Débil:**
  * *Vulnerabilidad:* Falta de restricciones de roles y almacenamiento inseguro de credenciales.
  * *Corrección:* Implementación de autenticación robusta mediante **BCrypt** para el hashing de contraseñas y un servicio de autorización (RBAC) para proteger las rutas administrativas.

---

## 🤖 El Papel de Microsoft Copilot en el Proceso
Microsoft Copilot se utilizó como herramienta colaborativa para:
* Analizar y detectar patrones de concatenación inseguros en el código base inicial.
* Generar las estructuras de validación de entradas y las consultas parametrizadas.
* Automatizar la creación de casos de prueba unitarios en NUnit para simular ataques de inyección y validar el control de acceso por roles.
