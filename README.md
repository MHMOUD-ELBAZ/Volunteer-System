# Hands of Hope - Volunteer Management System API

### **Overview**
The Hands of Hope Volunteer Management System is a backend API designed to streamline the connection between organizations and skilled volunteers. It allows organizations to post opportunities and manage applications, while volunteers can find opportunities that align with their skills and interests.

---

## **Features**
- CRUD operations for **volunteers**, **organizations**, **opportunities**, **applications**, and **reviews**.
- Relationships between key entities: `User`, `Organization`, `Volunteer`, `Opportunity`, `Application`, and `Review`.
- Ensures **data consistency** with cascading deletes, triggers, and database constraints.
- Built with database first approach which provides more control over database.
- Used **JWT** based authentication and role-based access for volunteers and organizations.
---

## **Enhanced-ERD**
![image](https://github.com/user-attachments/assets/97240fd5-c7bb-4e03-b645-f824e7de889c)

---

## **Architecture**
This project follows the **Clean Architecture** pattern to ensure scalability, maintainability, and separation of concerns. The main layers include:

2. **Business Layer**:
   - Contains definitions for all data transfer objects.
   - Contains business use cases and interfaces for all services.
   - Implements all services which include part of data access validations.
   - Contains definitions for mappers classes that used to map data between models and DTOs.

1. **Data Layer**:
   - Contains definitions for core data models. 
   - Manages data access and database connection.
   - Implements repositories for each entity with both LINQ and raw SQL approaches.

3. **API Layer**:
   - Exposes the RESTful APIs for CRUD operations.
   - Uses attribute-based routing and input validation.

---

## **Challenges**
- **Complex Relationships**:
  - Managing multi-level relationships (e.g., `Opportunity → Organization → ApplicationUser`) while maintaining performance.
- **Performance Optimization**:
  - Reduced network traffic with stored procedures for critical operations.
  - Used triggers to avoid the N+1 problem for calculating volunteer ratings.
    
---

