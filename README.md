# Elite ERP - API Project

## 📦 Setup Instructions

### How to Run the Project

1. Open a terminal inside the `API` project folder.
2. Run the following command: `dotnet run`

3. Use the **URL displayed in the CMD terminal** as it is to access the API.

---

## 🗂️ Project Structure

Elite_ERP/


* API -> controllers
* Core -> entities
* Infrastructure -> Repositories
* Service -> services layer



---

## 🔍 Filters, Sorting, and Logging Implementation

### Employee Filtering & Sorting

- When retrieving employees, **sorting by `name` or `hiredate` is required**.  
- All other filter options are **optional**.


---

### Logging

- Logs are recorded **only when a user is logged in**.
- Currently, the endpoints allow anonymous access (`[AllowAnonymous]`), but logs will capture user activity when authenticated.

---

## ⚙️ Business Rules & Assumptions

- You **must create a department first before creating an employee**.
- The API currently allows unauthenticated access, but production systems would typically restrict access with a token.


