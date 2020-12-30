# MonoTask
Job application task

#### Summary
develop a minimalistic application of your choice by following technologies and concepts mentioned above and requirements defined below.

### Requirements

Create a database with the following elements<br />
<ul>
<li>VehicleMake (Id,Name,Abrv) e.g. BMW,Ford,Volkswagen</li>
<li>VehicleModel (Id,MakeId,Name,Abrv) e.g. 128,325,X5 (BWM)</li>
</ul>

Create the solution (back-end) with the following projects and elements
<ul>
  <li>Project.Service
    <ul>
       <li>EF models for above database tables</li>
       <li>VehicleService class - CRUD for Make and Model (Sorting, Filtering & Paging)</li>
    </ul>
  </li>
   <li>Project.MVC
    <ul>
       <li>Make administration view (CRUD with Sorting, Filtering & Paging)</li>
       <li>Model administration view (CRUD with Sorting, Filtering (by make) & Paging)</li>
    </ul>
  </li>
</ul>
      
### Implementation details 
<ul>
<li>async/await should be enforced in all layers (async all the way)</li>
<li>all classes should be abstracted (have interfaces so that they can be unit tested)</li>
<li>IoC (Inversion of Control) and DI (Dependency Injection) should be enforced in all layers (constructor injection preferable)</li>
<li>Ninject DI container should be used (https://github.com/ninject/ninject/wiki)</li>
<li>Mapping should be done by using AutoMapper (http://automapper.org/)</li>
<li>EF 6, Core or above with Code First approach (EF Power Tools can be used) should be used</li>
<li>return view models rather than EF database models return proper Http status codes</li>
</ul>

