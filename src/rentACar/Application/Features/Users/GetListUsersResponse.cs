using System;
namespace Application.Features.Users;

public class GetListUsersResponse
{ 
    public string id { get; set; } 
    public string external_id { get; set; }
    public string primary_email_address_id { get; set; }
    public string primary_phone_number_id { get; set; }
    public string primary_web3_wallet_id { get; set; }
    public string username { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string profile_image_url { get; set; }
    public string image_url { get; set; }
    public bool has_image { get; set; } 
    public List<object> email_addresses { get; set; }
    public List<object> phone_numbers { get; set; }
    public List<object> web3_wallets { get; set; }
    public List<object> passkeys { get; set; }     
    public long last_sign_in_at { get; set; }
    public bool banned { get; set; }
    public bool locked { get; set; }
    public int? lockout_expires_in_seconds { get; set; } 
    public long? updated_at { get; set; }
    public long? created_at { get; set; }
    public bool? delete_self_enabled { get; set; }
    public bool? create_organization_enabled { get; set; }
    public int? create_organizations_limit { get; set; }
    public long? last_active_at { get; set; }
    public long? legal_accepted_at { get; set; }
}

