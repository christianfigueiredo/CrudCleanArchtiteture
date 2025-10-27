using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation
{
    public class EmployeeRepo : IEmployee
    {
        private readonly AppDbContext _context;
        public EmployeeRepo(AppDbContext context)
        {
           _context = context;
        }
        public async Task<ServiceResponse> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await SaveChangesAsync();
            return new ServiceResponse(true, "Employee added successfully.");

        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return new ServiceResponse(false, "Employee not found.");
            _context.Employees.Remove(employee);
            await SaveChangesAsync();
            return new ServiceResponse(true, "Employee deleted successfully.");
        }

        public async Task<List<Employee>> GetAsync() =>  await _context.Employees.AsNoTracking().ToListAsync();        

        public async Task<Employee> GetByIdAsync(int id) =>  await _context.Employees.FindAsync(id);

        public async Task<ServiceResponse> UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await SaveChangesAsync();
            return new ServiceResponse(true, "Employee updated successfully.");

        }
        private async Task SaveChangesAsync() =>  await _context.SaveChangesAsync();
        
    }
}
