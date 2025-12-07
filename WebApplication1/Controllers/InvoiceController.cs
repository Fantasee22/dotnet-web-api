using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class InvoiceController : ControllerBase
    {
        private readonly WebApplication1Context context;
        string connString = "Server=LAPTOP-6TPEADB9;Database=assessmentdb;Trusted_Connection=True;";

        public InvoiceController(WebApplication1Context context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("GetAllTrInvoice")]
        [AllowAnonymous]
        public async Task<ActionResult<ResListGeneric>> GetAllInvoicesAsync()
        {
            var table = new DataTable();
            using var conn = new SqlConnection(connString);
            using var cmd = new SqlCommand("SELECT * FROM TrInvoice", conn);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
                return NotFound("Data not found");

            table.Load(reader);

            List<TrInvoice> listRes = convertTrInvoiceDataTableToList(table);

            ResListGeneric res = new ResListGeneric();
            res.listRes = listRes;

            return Ok(res);
        }

        [HttpPost]
        [Route("GetTrInvoiceByInvoiceNo")]
        [AllowAnonymous]
        public async Task<ActionResult<TrInvoice?>> GetTrInvoiceByInvoiceNo(ReqByTrxNo req)
        {
            using var conn = new SqlConnection(connString);

            using var cmd = new SqlCommand("SELECT TOP 1 * FROM TrInvoice WHERE InvoiceNo = @InvoiceNo", conn);
            cmd.Parameters.AddWithValue("@InvoiceNo", req.trxNo);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
                return NotFound("Data not found");

            await reader.ReadAsync();

            var trInvoice = new TrInvoice
            {
                InvoiceNo = reader["InvoiceNo"].ToString(),
                InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]),
                InvoiceTo = reader["InvoiceTo"].ToString(),
                ShipTo = reader["ShipTo"].ToString(),
                SalesID = Convert.ToInt32(reader["SalesID"]),
                CourierID = Convert.ToInt32(reader["CourierID"]),
                PaymentType = Convert.ToInt32(reader["PaymentType"]),
                CourierFee = Convert.ToDecimal(reader["CourierFee"])
            };

            return Ok(trInvoice);
        }

        [HttpPost]
        [Route("GetTrInvoiceDetailByInvoiceNo")]
        [AllowAnonymous]
        public async Task<ActionResult<ResListGeneric>> GetTrInvoiceDetailByInvoiceNo(ReqByTrxNo req)
        {
            var table = new DataTable();
            using var conn = new SqlConnection(connString);
            
            using var cmd = new SqlCommand("SELECT * FROM TrInvoiceDetail WHERE InvoiceNo = @InvoiceNo", conn);
            cmd.Parameters.AddWithValue("@InvoiceNo", req.trxNo);
            
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
                return NotFound("Data not found");

            table.Load(reader);

            List<TrInvoiceDetail> listRes = convertTrInvoiceDetailDataTableToList(table);

            ResListGeneric res = new ResListGeneric();
            res.listRes = listRes;

            return Ok(res);
        }

        [HttpPost]
        [Route("GetAllmscourier")]
        [AllowAnonymous]
        public async Task<ActionResult<ResListGeneric>> GetAllmscourier()
        {
            var table = new DataTable();
            using var conn = new SqlConnection(connString);

            using var cmd = new SqlCommand("SELECT * FROM mscourier", conn);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
                return NotFound("Data not found");

            table.Load(reader);

            var listRes = table.AsEnumerable()
                          .Select(row => table.Columns.Cast<DataColumn>()
                              .ToDictionary(col => col.ColumnName, col => row[col]))
                          .ToList();

            ResListGeneric res = new ResListGeneric();
            res.listRes = listRes;

            return Ok(res);
        }

        [HttpPost]
        [Route("GetAllmspayment")]
        [AllowAnonymous]
        public async Task<ActionResult<ResListGeneric>> GetAllmspayment()
        {
            var table = new DataTable();
            using var conn = new SqlConnection(connString);

            using var cmd = new SqlCommand("SELECT * FROM mspayment", conn);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
                return NotFound("Data not found");

            table.Load(reader);

            var listRes = table.AsEnumerable()
                          .Select(row => table.Columns.Cast<DataColumn>()
                              .ToDictionary(col => col.ColumnName, col => row[col]))
                          .ToList();

            ResListGeneric res = new ResListGeneric();
            res.listRes = listRes;

            return Ok(res);
        }

        [HttpPost]
        [Route("GetAllmsproduct")]
        [AllowAnonymous]
        public async Task<ActionResult<ResListGeneric>> GetAllmsproduct()
        {
            var table = new DataTable();
            using var conn = new SqlConnection(connString);

            using var cmd = new SqlCommand("SELECT * FROM msproduct", conn);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
                return NotFound("Data not found");

            table.Load(reader);

            var listRes = table.AsEnumerable()
                          .Select(row => table.Columns.Cast<DataColumn>()
                              .ToDictionary(col => col.ColumnName, col => row[col]))
                          .ToList();

            ResListGeneric res = new ResListGeneric();
            res.listRes = listRes;

            return Ok(res);
        }

        [HttpPost]
        [Route("GetAllmssales")]
        [AllowAnonymous]
        public async Task<ActionResult<ResListGeneric>> GetAllmssales()
        {
            var table = new DataTable();
            using var conn = new SqlConnection(connString);

            using var cmd = new SqlCommand("SELECT * FROM mssales", conn);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
                return NotFound("Data not found");

            table.Load(reader);

            var listRes = table.AsEnumerable()
                          .Select(row => table.Columns.Cast<DataColumn>()
                              .ToDictionary(col => col.ColumnName, col => row[col]))
                          .ToList();

            ResListGeneric res = new ResListGeneric();
            res.listRes = listRes;

            return Ok(res);
        }

        [HttpPost]
        [Route("InsertTrInvoice")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertTrInvoice([FromBody] TrInvoice invoice)
        {
            if (invoice == null)
                return BadRequest("Invoice data is required");

            string query = @"
                INSERT INTO TrInvoice 
                (InvoiceNo, InvoiceDate, InvoiceTo, ShipTo, SalesID, CourierID, PaymentType, CourierFee)
                VALUES
                (@InvoiceNo, @InvoiceDate, @InvoiceTo, @ShipTo, @SalesID, @CourierID, @PaymentType, @CourierFee)
            ";

            try
            {
                using var conn = new SqlConnection(connString);
                using var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@InvoiceNo", invoice.InvoiceNo);
                cmd.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                cmd.Parameters.AddWithValue("@InvoiceTo", invoice.InvoiceTo);
                cmd.Parameters.AddWithValue("@ShipTo", invoice.ShipTo);
                cmd.Parameters.AddWithValue("@SalesID", invoice.SalesID);
                cmd.Parameters.AddWithValue("@CourierID", invoice.CourierID);
                cmd.Parameters.AddWithValue("@PaymentType", invoice.PaymentType);

                float totalWeight = invoice.details.Sum(d => d.Weight * d.Qty);
                decimal courierFee = await CalculateCourierFee(invoice.CourierID, totalWeight);
                cmd.Parameters.AddWithValue("@CourierFee", courierFee);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                await SaveInvoiceDetails(invoice.details, invoice.InvoiceNo);

                if (rowsAffected > 0)
                    return Ok(new { message = "Invoice inserted successfully" });
                else
                    return StatusCode(500, "Failed to insert invoice");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("UpdateTrInvoice")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateTrInvoice([FromBody] TrInvoice invoice)
        {
            if (invoice == null || string.IsNullOrWhiteSpace(invoice?.InvoiceNo))
                return BadRequest("Invoice data and InvoiceNo are required");

            string query = @"
                UPDATE TrInvoice
                SET 
                    InvoiceDate = @InvoiceDate,
                    InvoiceTo = @InvoiceTo,
                    ShipTo = @ShipTo,
                    SalesID = @SalesID,
                    CourierID = @CourierID,
                    PaymentType = @PaymentType,
                    CourierFee = @CourierFee
                WHERE InvoiceNo = @InvoiceNo
            ";

            try
            {
                using var conn = new SqlConnection(connString);
                using var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@InvoiceNo", invoice.InvoiceNo);
                cmd.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                cmd.Parameters.AddWithValue("@InvoiceTo", invoice.InvoiceTo);
                cmd.Parameters.AddWithValue("@ShipTo", invoice.ShipTo);
                cmd.Parameters.AddWithValue("@SalesID", invoice.SalesID);
                cmd.Parameters.AddWithValue("@CourierID", invoice.CourierID);
                cmd.Parameters.AddWithValue("@PaymentType", invoice.PaymentType);

                float totalWeight = invoice.details.Sum(d => d.Weight * d.Qty);
                decimal courierFee = await CalculateCourierFee(invoice.CourierID, totalWeight);
                cmd.Parameters.AddWithValue("@CourierFee", courierFee);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                await SaveInvoiceDetails(invoice.details, invoice.InvoiceNo);

                if (rowsAffected > 0)
                    return Ok(new { message = "Invoice updated successfully" });
                else
                    return NotFound(new { message = "Invoice not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private List<TrInvoice> convertTrInvoiceDataTableToList(DataTable table)
        {
            var list = new List<TrInvoice>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new TrInvoice
                {
                    InvoiceNo = row["InvoiceNo"].ToString(),
                    InvoiceDate = Convert.ToDateTime(row["InvoiceDate"]),
                    InvoiceTo = row["InvoiceTo"].ToString(),
                    ShipTo = row["ShipTo"].ToString(),
                    SalesID = Convert.ToInt32(row["SalesID"]),
                    CourierID = Convert.ToInt32(row["CourierID"]),
                    PaymentType = Convert.ToInt32(row["PaymentType"]),
                    CourierFee = Convert.ToDecimal(row["CourierFee"])
                });
            }

            return list;
        }

        private List<TrInvoiceDetail> convertTrInvoiceDetailDataTableToList(DataTable table)
        {
            var list = new List<TrInvoiceDetail>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new TrInvoiceDetail
                {
                    InvoiceNo = row["InvoiceNo"].ToString(),
                    ProductID = Convert.ToInt32(row["ProductID"]),
                    Weight = Convert.ToSingle(row["Weight"]),
                    Qty = Convert.ToInt16(row["Qty"]),
                    Price = Convert.ToDecimal(row["Price"])
                });
            }

            return list;
        }

        private async Task<decimal> CalculateCourierFee(int courierId, float totalWeight)
        {
            decimal courierFee = 0;

            string query = @"
                SELECT TOP 1 Price
                FROM ltcourierfee
                WHERE CourierID = @CourierID
                  AND StartKg <= @TotalWeight
                  AND (@TotalWeight <= EndKg OR EndKg IS NULL)
                ORDER BY StartKg ASC
            ";

            using var conn = new SqlConnection(connString);
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@CourierID", courierId);
            cmd.Parameters.AddWithValue("@TotalWeight", totalWeight);

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();

            if (result != null)
                courierFee = Convert.ToDecimal(result);

            return courierFee;
        }

        [HttpPost]
        [Route("GetCourierFee")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourierFee([FromBody] CourierFeeRequest req)
        {
            if (req == null || req.CourierID <= 0 || req.TotalWeight < 0)
                return BadRequest("Invalid input");

            try
            {
                decimal fee = await CalculateCourierFee(req.CourierID, req.TotalWeight);
                return Ok(new { CourierID = req.CourierID, TotalWeight = req.TotalWeight, CourierFee = fee });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task SaveInvoiceDetails(List<TrInvoiceDetail> details, string invoiceNo)
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            using var transaction = conn.BeginTransaction();

            try
            {
                using (var deleteCmd = new SqlCommand(
                    "DELETE FROM TrInvoiceDetail WHERE InvoiceNo = @InvoiceNo", conn, transaction))
                {
                    deleteCmd.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    await deleteCmd.ExecuteNonQueryAsync();
                }

                foreach (var d in details)
                {
                    using var insertCmd = new SqlCommand(
                        @"INSERT INTO TrInvoiceDetail (InvoiceNo, ProductID, Weight, Qty, Price)
                        VALUES (@InvoiceNo, @ProductID, @Weight, @Qty, @Price)", conn, transaction
                     );

                    insertCmd.Parameters.AddWithValue("@InvoiceNo", d.InvoiceNo);
                    insertCmd.Parameters.AddWithValue("@ProductID", d.ProductID);
                    insertCmd.Parameters.AddWithValue("@Weight", d.Weight);
                    insertCmd.Parameters.AddWithValue("@Qty", d.Qty);
                    insertCmd.Parameters.AddWithValue("@Price", d.Price);

                    await insertCmd.ExecuteNonQueryAsync();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }
    }
}
