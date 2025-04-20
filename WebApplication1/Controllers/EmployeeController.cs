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
using System.Globalization;

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
        [Route("GetAllEmployeePosition")]
        [AllowAnonymous]
        public async Task<ActionResult<List<EmployeePosition>>> GetAllEmployeePosition()
        {
            List<EmployeePosition> listEmpPos = await this.context.EmployeePosition.ToListAsync();
            ResListEmployeePosition resListEmpPos = new ResListEmployeePosition();
            foreach (EmployeePosition empPos in listEmpPos)
            {
                resListEmpPos.listEmployeePosition.Add(new ResEmployeePosition()
                {
                    Id = empPos.Id,
                    Name = empPos.Name,
                    Code = empPos.Code
                });
            }
            return Ok(resListEmpPos);
        }

        [HttpPost]
        [Route("GetEmployeePositionById")]
        [AllowAnonymous]
        public async Task<ActionResult<Employee>> GetEmployeePositionById([FromBody] ReqById req)
        {
            EmployeePosition empPos = await this.context.EmployeePosition.Where(x => x.Id == req.Id).FirstOrDefaultAsync();
            ResEmployeePosition resEmpPos = new ResEmployeePosition();
            if (empPos is not null)
            {
                resEmpPos = new ResEmployeePosition()
                {
                    Id = empPos.Id,
                    Name = empPos.Name,
                    Code = empPos.Code
                };
            }

            return Ok(resEmpPos);
        }

        [HttpPost]
        [Route("GetAllOfficeBranch")]
        [AllowAnonymous]
        public async Task<ActionResult<List<OfficeBranch>>> GetAllOfficeBranch()
        {
            List<OfficeBranch> listOfcBrc = await this.context.OfficeBranch.ToListAsync();
            ResListOfficeBranch resListOfcBrc = new ResListOfficeBranch();
            foreach (OfficeBranch ofcBrc in listOfcBrc)
            {
                resListOfcBrc.listOfficeBranch.Add(new ResOfficeBranch()
                {
                    Id = ofcBrc.Id,
                    Name = ofcBrc.Name,
                    Code = ofcBrc.Code,
                    Address = ofcBrc.Address
                });
            }
            return Ok(resListOfcBrc);
        }

        [HttpPost]
        [Route("GetOfficeBranchById")]
        [AllowAnonymous]
        public async Task<ActionResult<Employee>> GetOfficeBranchById([FromBody] ReqById req)
        {
            OfficeBranch ofcBrc = await this.context.OfficeBranch.Where(x => x.Id == req.Id).FirstOrDefaultAsync();
            ResOfficeBranch resOfcBrc = new ResOfficeBranch();
            if (ofcBrc is not null)
            {
                resOfcBrc = new ResOfficeBranch()
                {
                    Id = ofcBrc.Id,
                    Name = ofcBrc.Name,
                    Code = ofcBrc.Code,
                    Address = ofcBrc.Address
                };
            }

            return Ok(resOfcBrc);
        }

        [HttpPost]
        [Route("GetAllUploadFile")]
        [AllowAnonymous]
        public async Task<ActionResult<List<OfficeBranch>>> GetAllUploadFile()
        {
            List<UploadFile> listUplFile = await this.context.UploadFile.ToListAsync();
            ResListUploadFile resListUplFile = new ResListUploadFile();
            foreach (UploadFile uplFile in listUplFile)
            {
                resListUplFile.listUploadFile.Add(new ResUploadFile()
                {
                    Id = uplFile.Id,
                    Name = uplFile.Name,
                    Status = uplFile.Status,
                    Base64Data = uplFile.Base64Data
                });
            }
            return Ok(resListUplFile);
        }

        [HttpPost]
        [Route("ExecSpGenericFilter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<EmployeeDetail>>> ExecSpGenericFilter(ReqSpWithFilter req)
        {
            var employeDetail = await this.context.Set<EmployeeDetail>()
                .FromSqlRaw("EXEC " + req.SpName + " @Filter = '" + req.Filter + "'") //sp_GetEmployeeDetailWithFilter
                .ToListAsync();

            return Ok(employeDetail);
        }

        [HttpPost]
        [Route("UploadFileExcel")]
        [AllowAnonymous]
        public async Task<ActionResult> UploadFileExcel(ReqFile req)
        {
            await this.context.UploadFile.AddAsync(new UploadFile()
            {
                Name = req.Name,
                Status = "NEW",
                Base64Data = req.Base64Content
            });

            await this.context.SaveChangesAsync();

            try
            {
                List<string[]> rows = new List<string[]>();
                byte[] fileBytes = Convert.FromBase64String(req.Base64Content);
                MemoryStream stream = new MemoryStream(fileBytes);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 1; row <= rowCount; row++)
                    {
                        var rowData = new string[colCount];

                        for (int col = 1; col <= colCount; col++)
                        {
                            rowData[col - 1] = worksheet.Cells[row, col].Text;
                        }
                        if (!string.IsNullOrWhiteSpace(rowData[1].ToString()))
                            rows.Add(rowData);
                    }

                    UploadFile? uploadFile = await (from a in context.UploadFile
                                                    where a.Name == req.Name && a.Status == "NEW"
                                                    select a).FirstOrDefaultAsync();

                    

                    if (uploadFile is not null)
                    {
                        if (rows.Count <= 0)
                        {
                            uploadFile.Status = "EMPTY";
                        } 
                        else
                        {
                            await this.handleOfficeBranch(rows);
                            await this.handleEmployeePosition(rows);
                            await this.handleEmployee(rows, uploadFile.Id);

                            uploadFile.Status = "DONE";
                        }
                        
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                UploadFile? uploadFile = await (from a in context.UploadFile
                                                where a.Name == req.Name && a.Status == "NEW"
                                                select a).FirstOrDefaultAsync();
                if (uploadFile is not null)
                {
                    uploadFile.Status = "ERROR";
                }

                return BadRequest(e.Message);
            }

            return Ok(new ResMsg() { Msg = "Success" });
        }

        private async Task handleEmployee(List<string[]> rows, long uploadFileId)
        {
            List<string> addedCode = new List<string>();

            for (int i = 1; i < rows.Count; i++)
            {
                if (addedCode.Contains(rows[i][1].ToString().Trim())) continue;
                Employee? employee = await (from a in context.Employee
                                           where a.Code == rows[i][1].ToString().Trim()
                                           select a).FirstOrDefaultAsync();

                OfficeBranch? officeBranch = await (from a in context.OfficeBranch
                                                    where a.Code == rows[i][10].ToString().Trim()
                                                    select a).FirstOrDefaultAsync();

                EmployeePosition? employeePosition = await (from a in context.EmployeePosition
                                                            where a.Code == rows[i][8].ToString().Trim()
                                                            select a).FirstOrDefaultAsync();

                if (employee is null)
                {
                    Employee addEmployee = new Employee()
                    {
                        Name = rows[i][0].ToString(),
                        Code = rows[i][1].ToString(),
                        IsContract = rows[i][2].ToString().ToLower() == "1" || rows[i][2].ToString().ToLower() == "true",
                        StartDt = DateTime.TryParseExact(rows[i][3].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tempDate) ? tempDate : null,
                        EndDt = DateTime.TryParseExact(rows[i][4].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tempDate2) ? tempDate2 : null,
                        Address = rows[i][5].ToString().Trim(),
                        Phone = rows[i][6].ToString().Trim(),
                        OfficeBranchId = officeBranch?.Id,
                        EmployeePositionId = employeePosition?.Id,
                        UploadFileId = uploadFileId
                    };
                    await this.context.Employee.AddAsync(addEmployee);
                    addedCode.Add(rows[i][1].ToString().Trim());
                }
                else
                {
                    employee.Name = rows[i][0].ToString();
                    employee.IsContract = rows[i][2].ToString().ToLower() == "1" || rows[i][2].ToString().ToLower() == "true";
                    employee.StartDt = DateTime.TryParseExact(rows[i][3].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tempDate) ? tempDate : null;
                    employee.EndDt = DateTime.TryParseExact(rows[i][4].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tempDate2) ? tempDate2 : null;
                    employee.Address = rows[i][5].ToString().Trim();
                    employee.Phone = rows[i][6].ToString().Trim();
                    employee.OfficeBranchId = officeBranch?.Id;
                    employee.EmployeePositionId = employeePosition?.Id;
                    employee.UploadFileId = uploadFileId;
                }
            }
            await this.context.SaveChangesAsync();
        }

        private async Task handleOfficeBranch(List<string[]> rows)
        {
            List<string> addedCode = new List<string>();

            for (int i = 1; i < rows.Count; i++)
            {
                if (addedCode.Contains(rows[i][10].ToString().Trim())) continue;

                OfficeBranch? officeBranch = await (from a in context.OfficeBranch
                                                   where a.Code == rows[i][10].ToString().Trim()
                                                   select a).FirstOrDefaultAsync();
                if (officeBranch is null)
                {
                    await this.context.OfficeBranch.AddAsync(new OfficeBranch()
                    {
                        Name = rows[i][9].ToString().Trim(),
                        Code = rows[i][10].ToString().Trim(),
                        Address = rows[i][11].ToString().Trim()
                    });
                    addedCode.Add(rows[i][10].ToString().Trim());
                } 
                else
                {
                    officeBranch.Name = rows[i][9].ToString().Trim();
                    officeBranch.Address = rows[i][11].ToString().Trim();
                }
            }
            await this.context.SaveChangesAsync();
        }

        private async Task handleEmployeePosition(List<string[]> rows)
        {
            List<string> addedCode = new List<string>();

            for (int i = 1; i < rows.Count; i++)
            {
                if (addedCode.Contains(rows[i][8].ToString().Trim())) continue;

                EmployeePosition? employeePosition = await (from a in context.EmployeePosition
                                                           where a.Code == rows[i][8].ToString().Trim()
                                                           select a).FirstOrDefaultAsync();

                if (employeePosition is null)
                {
                    await this.context.EmployeePosition.AddAsync(new EmployeePosition()
                    {
                        Name = rows[i][7].ToString().Trim(),
                        Code = rows[i][8].ToString().Trim(),
                    });
                    addedCode.Add(rows[i][8].ToString().Trim());
                }
                else
                {
                    employeePosition.Name = rows[i][7].ToString().Trim();
                }
            }
            await this.context.SaveChangesAsync();
        }

    }
}
