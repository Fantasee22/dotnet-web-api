using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Model;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EmployeeController : ControllerBase
    {
        private readonly WebApplication1Context context;

        public EmployeeController(WebApplication1Context context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("GetAllEmployee")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
            List<Employee> listEmp = await this.context.Employee.ToListAsync();
            ResListEmployee resListEmp = new ResListEmployee();
            foreach (Employee emp in listEmp)
            {
                resListEmp.listEmployee.Add(new ResEmployee()
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Code = emp.Code,
                    IsContract = emp.IsContract,
                    StartDt = emp.StartDt,
                    EndDt = emp.EndDt,
                    Address = emp.Address,
                    Phone = emp.Phone,
                    EmployeePositionId = emp.EmployeePositionId,
                    OfficeBranchId = emp.OfficeBranchId,
                    UploadFileId = emp.UploadFileId
                });
            }
            return Ok(resListEmp);
        }

        [HttpPost]
        [Route("GetEmployeeById")]
        [AllowAnonymous]
        public async Task<ActionResult<Employee>> GetEmployeeById([FromBody] ReqById req)
        {
            Employee emp = await this.context.Employee.Where(x => x.Id == req.Id).FirstOrDefaultAsync();
            ResEmployee resEmp = new ResEmployee();
            if (emp is not null)
            {
                resEmp = new ResEmployee()
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Code = emp.Code,
                    IsContract = emp.IsContract,
                    StartDt = emp.StartDt,
                    EndDt = emp.EndDt,
                    Address = emp.Address,
                    Phone = emp.Phone,
                    EmployeePositionId = emp.EmployeePositionId,
                    OfficeBranchId = emp.OfficeBranchId,
                    UploadFileId = emp.UploadFileId
                };
            }

            return Ok(resEmp);
        }

        [HttpPost]
        [Route("UploadFileExcel")]
        [AllowAnonymous]
        public async Task<ActionResult> UploadFileExcel(ReqFile req)
        {
            try
            {
                List<string[]> rows = new List<string[]>();
                byte[] fileBytes = Convert.FromBase64String(req.Base64Content);
                MemoryStream stream = new MemoryStream(fileBytes);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 1; row <= rowCount; row++)
                    {
                        var rowData = new string[colCount];

                        for (int col = 1; col <= colCount; col++)
                        {
                            rowData[col - 1] = worksheet.Cells[row, col].Text;
                        }

                        rows.Add(rowData);
                    }

                    //using (StreamReader sr = new(stream))
                    //{
                    //    string line;
                    //    while ((line = sr.ReadLine()) != null)
                    //    {
                    //        string[] data = line.Split(',');
                    //        rows.Add(data);
                    //    }
                    //}

                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}
