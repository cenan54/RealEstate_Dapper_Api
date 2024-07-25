﻿namespace RealEstate_Dapper_UI.Dtos.MessageDtos
{
    public class ResultInBoxMessageDto
    {
        public int MessageID { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Detail { get; set; }
        public DateTime SendDate { get; set; }
        public bool IdRead { get; set; }
        public string UserImageUrl { get; set; }
    }
}
