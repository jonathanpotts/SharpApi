namespace SharpApi.Aws.Lambda
{
    public class CognitoContext
    {
        public string CognitoIdentityPoolId { get; set; }
        public string AccountId { get; set; }
        public string CognitoIdentityId { get; set; }
        public string Caller { get; set; }
        public string SourceIP { get; set; }
        public string AccessKey { get; set; }
        public string CognitoAuthenticationType { get; set; }
        public string CognitoAuthenticationProvider { get; set; }
        public string UserArn { get; set; }
        public string UserAgent { get; set; }
        public string User { get; set; }
    }
}
