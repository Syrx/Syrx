# Solution Layouts

Although not specific to Syrx, we recommend following the below layout for your solutions as it allows your code to grow in an intuitive, structured way which follows some simple conventions and makes for much easier maintenance.


### Fictional Context

For this document, we'll use a fictional project called _Felix_. The Felix project uses a simple N-tier architecture with:    
* **Databases**: Databases to persist and retrieve data. 
* **Models:** Immutable types representing domain models.     
* **Repositories:** Hydrate and persist these models.
* **Services:** Compose different services/models together for sophisticated workflows.
* **APIs:** Web APIs exposing the Felix services. 

--- 

## Basic Layout

```
Felix.sln
- src
 -- Felix.Extensions.Installers
 -- Felix.Models
 -- Felix.Repositories
 -- Felix.Services
 -- Felix.Web
- tests
 -- integration
  --- Felix.Repositories.Tests.Integration
-- unit
  --- Felix.Models.Tests.Unit 
  --- Felix.Repositories.Tests.Unit
  --- Felix.Services.Tests.Unit
  --- Felix.Web.Tests.Unit
```

---

## Models
The Models project is where you define the domain data models which will be passed throughout the stack. 
Immutable types are preferred. 