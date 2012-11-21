using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TweetSharp;

namespace Doig.Blog.Franklin.Controllers 
{
    public class Tweet
    {
        public DateTime CreatedDate { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
    }

    public class TweetsController : ApiController
    {
        private const string TWITTER_CONSUMER_KEY = "YOUR CONSUMER KEY";
        private const string TWITTER_CONSUMER_SECRET = "YOUR CONSUMER SECRET";
        private const string OAUTH_TOKEN_SECRET = "BROADCAST BABBLE TOKEN SECRET";
        private const string OAUTH_TOKEN = "BROADCAST BABBLE TOKEN";

        // GET api/values
        public IEnumerable<Tweet> Get()
        {
            var tweets = new List<Tweet>();

            var service = new TwitterService(TWITTER_CONSUMER_KEY, TWITTER_CONSUMER_SECRET);
            service.AuthenticateWith(OAUTH_TOKEN, OAUTH_TOKEN_SECRET);

            foreach (var serviceTweet in service.ListTweetsOnHomeTimeline(5))
            {
                var tweet = new Tweet()
                {
                    CreatedDate = serviceTweet.CreatedDate,
                    Id = serviceTweet.Id.ToString(),
                    Text = serviceTweet.Text,
                    UserName = serviceTweet.Author.ScreenName
                };
                tweets.Add(tweet);
            }
            return tweets;
        }
    }
}