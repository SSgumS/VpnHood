﻿using VpnHood.Common.Utils;

namespace VpnHood.Client.App.ClientProfiles;

public class ClientProfileBaseInfo(ClientProfile clientProfile)
{
    public Guid ClientProfileId { get; private set; } = clientProfile.ClientProfileId;
    public string ClientProfileName { get; private set; } = GetTitle(clientProfile);
    public string? RegionId { get; private set; } = clientProfile.RegionId;
    public string? SupportId { get; private set; } = clientProfile.Token.SupportId;

    private static string GetTitle(ClientProfile clientProfile)
    {
        var token = clientProfile.Token;

        if (!string.IsNullOrWhiteSpace(clientProfile.ClientProfileName))
            return clientProfile.ClientProfileName;

        if (!string.IsNullOrWhiteSpace(token.Name))
            return token.Name;

        if (token.ServerToken is { IsValidHostName: false, HostEndPoints.Length: > 0 })
            return VhUtil.RedactEndPoint(token.ServerToken.HostEndPoints.First());

        return VhUtil.RedactHostName(token.ServerToken.HostName);
    }
}