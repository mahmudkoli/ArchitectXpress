using ArchitectXpress.Models;
using MassTransit;
namespace ArchitectXpressBackground.RabbitMQ;

public class ProfileConsumer : IConsumer<ProfileKoli>
{
    public ProfileConsumer(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    public async Task Consume(ConsumeContext<OrganizationMessageDto> context)
    {
        var comsOrg = context.Message;

        Console.WriteLine($"Organization Consumer received: {comsOrg.OrganizationGuid}, {comsOrg.OrganizationName}");

        var result = await _organizationService.UpdateOrganizationInfoByConsumer(new Organization
        {
            OrganizationGuid = comsOrg.OrganizationGuid,
            OrganizationName = comsOrg.OrganizationName,
            Active = comsOrg.Active,
            AgencyORI = comsOrg.AgencyORI
        }, comsOrg.ParentOrganizationGuidFk, comsOrg.OrganizationMainLocationGuidFk);

        Console.WriteLine(result.IsSuccess ? $"Successfully updated: {result.Message}" : $"Update failed: {result.Message}");

        await Task.CompletedTask;
    }
}