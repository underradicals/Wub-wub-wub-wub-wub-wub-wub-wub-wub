# Email Verification

## ✅ 1. Syntax & Format Validation
- First, they use regular expressions and standard rules to verify the structure of the email address:
- Valid characters
- Single @
- Proper domain name format

## ✅ 2. DNS Lookup for Domain
- They check if the domain has valid MX (Mail Exchange) records:
- Queries DNS to ensure the domain exists.
- Confirms that it can receive email.
```bash
dig MX example.com 
```
> If the domain has no MX records → the email is invalid.

## ✅ 3. SMTP Handshake (Without Sending Email)
**This is the critical step:**
They open a connection to the mail server and simulate an SMTP handshake like this:
```bash
HELO verifier.com
MAIL FROM:<verify@verifier.com>
RCPT TO:<target@example.com>
```
Then they wait for the server response to RCPT TO.
- If the server responds with 250 OK, the address is valid.
- If 550 No such user, it’s invalid.
- If greylisted or deferred, it may retry later.

⚠️ <small>No email is actually sent, because they never reach the DATA phase of SMTP.</small>

## ✅ 4. Role-based & Disposable Address Detection
They maintain internal databases or heuristics to detect:
- Role-based addresses: e.g., admin@, info@, support@
- Temporary/disposable services: e.g., @mailinator.com, @10minutemail.com

## ✅ 5. Catch-All Domain Detection
Some domains accept all emails, even if the mailbox doesn't exist. These are called catch-all domains.
In this case, services:
- Attempt multiple RCPT TO requests with fake and real-looking addresses.
- Infer based on patterns or known behavior (some use AI heuristics).