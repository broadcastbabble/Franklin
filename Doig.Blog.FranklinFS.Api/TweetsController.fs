namespace Doig.Blog.FranklinFS.Api.Controllers

open System
open System.Runtime.Serialization
open System.Web
open System.Web.Mvc
open System.Net.Http
open System.Web.Http
open TweetSharp

[<DataContract(Name="Tweet")>]
type Tweet = { [<field: DataMember(Name="CreatedDate")>] CreatedDate : DateTime
               [<field: DataMember(Name="Id")>] Id : string
               [<field: DataMember(Name="Text")>] Text : string
               [<field: DataMember(Name="UserName")>] UserName : string }

type TweetsController() =
    inherit ApiController()

    let access_token = "315285408-tU3AwBCLzHDa6noqO9W1H7p1oKrapm0M0l81NBHd"
    let access_token_secret = "YLQEQsKK7cmsC7MkP41IlVs6Fljh476WcaR4FIThY"
    let consumer_key = "VdDanV31kHit2KcH7s1WmQ"
    let consumer_secret = "zdc7aRg4H8pp4ZvLBOVIOufGLZujZJ6issKqmjFaY"

    // GET /api/tweets
    member x.Get() = 
        let service = new TwitterService(consumer_key, consumer_secret)
        service.AuthenticateWith(access_token, access_token_secret)
        let tweets = query { for t in service.ListTweetsOnHomeTimeline(5) do
                             select { CreatedDate = t.CreatedDate;
                                      Id = t.Id.ToString();
                                      Text = t.Text;
                                      UserName = t.Author.ScreenName }}
        tweets