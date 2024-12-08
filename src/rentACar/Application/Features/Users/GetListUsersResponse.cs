using System;
namespace Application.Features.Users; 
 
public class EmailAddress
{
    public string id { get; set; }
    public string @object { get; set; }
    public string email_address { get; set; }
    public bool reserved { get; set; }
    public Verification verification { get; set; }
    public List<LinkedTo> linked_to { get; set; }
    public long created_at { get; set; }
    public long updated_at { get; set; }
}

public class ExternalAccount
{
    public string @object { get; set; }
    public string id { get; set; }
    public string google_id { get; set; }
    public string approved_scopes { get; set; }
    public string email_address { get; set; }
    public string given_name { get; set; }
    public string family_name { get; set; }
    public string picture { get; set; }
    public string username { get; set; }
    public PublicMetadata public_metadata { get; set; }
    public object label { get; set; }
    public long created_at { get; set; }
    public long updated_at { get; set; }
    public Verification verification { get; set; }
}

public class LinkedTo
{
    public string type { get; set; }
    public string id { get; set; }
}

public class PhoneNumber
{
    public string id { get; set; }
    public string @object { get; set; }
    public string phone_number { get; set; }
    public bool reserved_for_second_factor { get; set; }
    public bool default_second_factor { get; set; }
    public bool reserved { get; set; }
    public Verification verification { get; set; }
    public List<object> linked_to { get; set; }
    public object backup_codes { get; set; }
    public long created_at { get; set; }
    public long updated_at { get; set; }
}

public class PrivateMetadata
{
}

public class PublicMetadata
{
}

public class GetListUsersResponse
{
    public string id { get; set; }
    public string @object { get; set; }
    public string username { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string image_url { get; set; }
    public bool has_image { get; set; }
    public string primary_email_address_id { get; set; }
    public string primary_phone_number_id { get; set; }
    public object primary_web3_wallet_id { get; set; }
    public bool password_enabled { get; set; }
    public bool two_factor_enabled { get; set; }
    public bool totp_enabled { get; set; }
    public bool backup_code_enabled { get; set; }
    public List<EmailAddress> email_addresses { get; set; }
    public List<PhoneNumber> phone_numbers { get; set; }
    public List<object> web3_wallets { get; set; }
    public List<object> passkeys { get; set; }
    public List<ExternalAccount> external_accounts { get; set; }
    public List<object> saml_accounts { get; set; }
    public List<object> enterprise_accounts { get; set; }
    public PublicMetadata public_metadata { get; set; }
    public PrivateMetadata private_metadata { get; set; }
    public UnsafeMetadata unsafe_metadata { get; set; }
    public object external_id { get; set; }
    public long last_sign_in_at { get; set; }
    public bool banned { get; set; }
    public bool locked { get; set; }
    public object lockout_expires_in_seconds { get; set; }
    public int verification_attempts_remaining { get; set; }
    public long created_at { get; set; }
    public long updated_at { get; set; }
    public bool delete_self_enabled { get; set; }
    public bool create_organization_enabled { get; set; }
    public long last_active_at { get; set; }
    public object mfa_enabled_at { get; set; }
    public object mfa_disabled_at { get; set; }
    public object legal_accepted_at { get; set; }
    public string profile_image_url { get; set; }
}

public class UnsafeMetadata
{
}

public class Verification
{
    public string status { get; set; }
    public string strategy { get; set; }
    public object attempts { get; set; }
    public object expire_at { get; set; }
}

