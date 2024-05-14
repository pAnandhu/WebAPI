using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("Callbacks/Approvals")]
    public class TestCallBackController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] Approval approval

        )
        {

            string logPath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");
            var response = JsonSerializer.Serialize(approval);
            using (StreamWriter writer = new StreamWriter(logPath))
            {
                writer.WriteLine(response);
            }
            Task.WaitAll();
            return Ok();

        }
    }

    public class Approval
    {
        [JsonPropertyName("adaptiveCard")]
        public string? AdaptiveCard { get; set; }

        [JsonPropertyName("approvers")]
        public List<Approver> Approvers { get; set; } = new();

        [JsonPropertyName("completionDate")]
        public DateTimeOffset? CompletionDate { get; set; }

        [JsonPropertyName("details")]
        public string? Details { get; set; }

        [JsonPropertyName("expirationDate")]
        public DateTimeOffset ExpirationDate { get; set; }

        [JsonPropertyName("name")]
        public Guid Id { get; set; }

        /// <summary>
        /// Comma space delimited list of all outcomes when multiple responses.
        /// </summary>
        [JsonPropertyName("outcome")]
        public string? Outcome { get; set; }

        [JsonPropertyName("priority")]
        public string Priority { get; set; }

        [JsonPropertyName("requestDate")]
        public DateTimeOffset RequestDate { get; set; }

        [JsonPropertyName("respondLink")]
        public Uri? RespondLink { get; set; }

        [JsonPropertyName("responses")]
        public List<ApprovalResponse> Responses { get; set; } = new();

        [JsonPropertyName("responseSummary")]
        public string? ResponseSummary { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
    public partial class Approver
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("tenantId")]
        public Guid TenantId { get; set; }

        [JsonPropertyName("userPrincipalName")]
        public string UserPrincipalName { get; set; }
    }
    public partial class ApprovalResponse
    {
        [JsonPropertyName("responder")]
        public Approver Responder { get; set; }

        [JsonPropertyName("requestDate")]
        public DateTimeOffset RequestDate { get; set; }

        [JsonPropertyName("responseDate")]
        public DateTimeOffset ResponseDate { get; set; }

        [JsonPropertyName("approverResponse")]
        public string ApproverResponse { get; set; }

        [JsonPropertyName("comments")]
        public string? Comments { get; set; }
    }
}
