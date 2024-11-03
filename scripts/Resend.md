## Resend tutorial

#### Requirements:

- **Registration** in [Resend site](https://resend.com/)
- **Domain** - use hosting site where you have your own domain/site

* * *

### Registration and setup

- Registration: [here](https://resend.com/) - easy
- Add your domain and DNS: [here](https://resend.com/domains) - you'll need to alre4ady have domain and to setup it
- Create your api key: [here](https://resend.com/api-keys) - you will use in this library

### Your domain

*[This](https://somee.com) is just an example - you may use different site to register domain*
- Registration: [here](https://somee.com/DOKA/Identity/Account/Login) - easy
- Add DNS records: [here](https://somee.com/DOKA/DOU/DNS/DnsRecordList/0/All/False/Type/True) - look in [resend](https://resend.com/domains) and setup it

* * *

#### Simple small code snippet how to use it

```
var sender = new ResendSender("api key created in resend.com", "your domain's default email");
var emailResult = await sender.SendEmail(senderEmail, [recipientEmail], "Title", "<h1>Content</h1>");
```
