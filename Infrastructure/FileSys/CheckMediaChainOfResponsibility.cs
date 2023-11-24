namespace Infrastructure.FileSys
{
    public static partial class CheckMediaChainOfResponsibility
    {
        public static async Task<MediaContentDTO> CheckInadmissibleContent(
            this MediaContentDTO mediaContent,
            IAntiVirus antiVirus,
            IAzureAdultContentDetection azureAdultContentDetection,
            CancellationToken cancellationToken)
        {
            
            return (await (mediaContent.CheckContentForPorn(azureAdultContentDetection)))
                               .CheckContentForVirus(antiVirus)
                               .WaitAsync(cancellationToken).Result
                               .CheckContentForViolence(azureAdultContentDetection)
                               .WaitAsync(cancellationToken).Result;
        }
        private static async Task<MediaContentDTO> CheckContentForPorn(
            this MediaContentDTO mediaContent,
            IAzureAdultContentDetection azureAdultContentDetection)
        {
            bool isNot = true;
            mediaContent.PornCheckProcessed(isNot);
            return mediaContent;
        }
        private static async Task<MediaContentDTO> CheckContentForVirus(
            this MediaContentDTO mediaContent,
            IAntiVirus antiVirus)
        {
            bool isNot = await antiVirus.CheckIfBinaryIsNotVirusContent();
            mediaContent.VirusCheckProcessed(isNot);
            return mediaContent;
        }
        private static async Task<MediaContentDTO> CheckContentForViolence(
            this MediaContentDTO mediaContent,
            IAzureAdultContentDetection azureAdultContentDetection)
        {
            bool isNot = true;
            mediaContent.ViolentCheckProcessed(isNot);
            return mediaContent;
        }
    }
}
