<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tr="urn:org:pesc:message:TranscriptRequest:v1.0.0" exclude-result-prefixes="tr">
  <xsl:output method="html" doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd" indent="yes"/>
  <xsl:template match="/">
    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <title>Alberta Education Request for Official Transcripts</title>
        <style type="text/css">
          body { font-family: sans-serif; font-size: 10pt; width: 800px; }
          .logo { float: left; }
          .header { padding-top: 20px; font-size: 16pt; text-align: center; }
          .title { font-size: 12pt; font-weight: bold; font-style: italic; }
          .banner { clear: left; border: 2px solid; padding-left: 4px; padding-right: 4px; width: 800px; background-color: #DDDDDD; }
          .section { border-top: 2px solid; border-left: 2px solid; border-right: 1px solid; border-bottom: 1px solid; overflow: auto; width: 800px; }
          .field { float: left; border-right: 1px solid; border-bottom: 1px solid; padding-left: 4px; padding-right: 4px; }
          .label { font-size: 8pt; }
          .content { font-weight: bold; }
          .official { background-color: #DDDDDD; }
          .notice { font-size: 12pt; }
          .instructions { text-align: center; }
          #surname { width: 342px; }
          #givenName { width: 440px; }
          #maidenName { width: 791px; }
          #asn { width: 172px; }
          #dateOfBirth { width: 172px; }
          #email { width: 248px }
          #telephoneNumber { width: 172px; }
          #mailingAddress { width: 791px; }
          #city { width: 292px; }
          #province { width: 309px; }
          #postalCode { width: 172px; }
          #highSchool { width: 791px; }
          #highSchoolCity { width: 389px; }
          #lastYearAttended { width: 212px; }
          #lastGradeAttended { width: 172px; }
          #timeCodeHeader { text-align: center; height: 50px; }
          .timeCode { width: 80px; }
          #numberOfCopiesHeader { text-align: center; height: 50px; }
          .numberOfCopies { width: 80px; }
          #destinationCodeHeader { text-align: center; height: 50px; }
          .destinationCode { width: 75px; }
          #transcriptLanguageHeader { text-align: center; height: 50px; }
          .transcriptLanguage { width: 60px; }
          #destinationNameHeader { text-align: center; height: 50px; }
          .destinationName { width: 225px; }
          #destinationAddressHeader { text-align: center; height: 50px; }
          .destinationAddress { width: 226px; }
          #transcriptCount { width: 791px; text-align: center; }
          #paymentMethod { width: 200px; height: 28px; border-right: none; }
          #cashDebit { width: 100px; height: 28px; border-right: none; }
          #cheque { width: 280px; height: 28px; border-right: none; }
          #visa { width: 90px; height: 28px; border-right: none; }
          #masterCard { width: 89px; height: 28px; }
          #cardNumber { width: 391px; }
          #expiryDate { width: 391px; }
          #cardholder { width: 391px; }
          #signature { width: 391px; }
          #aknowledgement { width: 791px; }
          #studentSignature { width: 391px; }
          #date { width: 391px; }
          #relationship { width: 592px; }
          #authorizationTelephoneNumber { width: 190px; height: 42px; }
          #authorizationSurname { width: 267px; }
          #authorizationFirstName { width: 267px; }
          #authorizationMiddleName { width: 239px; }
          #authorizationSignature { width: 543px; }
          #authorizationDate { width: 239px; }
          #officeUseDriversLicence { width: 120px; border-right: none; border-bottom: none; }
          #studentBirthCertificate { width: 110px; border-right: none; border-bottom: none; }
          #passport { width: 80px; border-right: none; border-bottom: none; }
          #parentGuardianVitalStatistics { width: 113px; border-right: none; border-bottom: none; }
          #other { width: 120px; border-right: none; border-bottom: none; }
          #initial { width: 80px; border-right: none; border-bottom: none; }
          #initialDate { width: 120px; border-bottom: none; }
          #officeAddress { width: 600px; border-right: none; }
          #officePhone { width: 183px; }
        </style>
      </head>
      
      <body>
        <img class="logo" src="images/AELogo.png"/>
        <div class="header">Request for Official Transcripts</div>
        <div class="banner">
          The personal information collected on this form is collected pursuant to Section 33(c) of the <b>Freedom of Information
          and Protection of Privacy Act (RSA 2000, c. F-25) Act</b> and is being used for the processing, handling and issuance
          of the appropriate official transcripts in accordance with the information supplied on the form. Any questions
          concerning the collection of this personal information may be directed to the Information Services Help Desk (<b>44
          Capital Boulevard, 10044 - 108 Street NW, Edmonton, Alberta T5J 5E6</b>) who may be reached at <b>(780) 427-5318</b>
          or Toll Free at <b>310-0000</b> (within Alberta).
        </div>
        <div class="notice">Transcripts can also be ordered online at http://education.alberta.ca/students/transcripts.aspx</div>
        <div class="instructions">PLEASE PRINT CLEARLY IN INK. Read instructions on the back of this form.</div>
        <br/>
        <div class="title">STUDENT PERSONAL INFORMATION - This section <u>must</u> be completely filled in.</div>
        <div class="section">
          <div id="surname" class="field">
            <div class="label">
              Surname (Last Name)
              <xsl:choose>
                <xsl:when test="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Gender/GenderCode = 'Male'">
                  <img src="images/checkbox_checked.png"/>
                </xsl:when>
                <xsl:otherwise>
                  <img src="images/checkbox.png"/>
                </xsl:otherwise>
              </xsl:choose>
              Mr.
              <img src="images/checkbox.png"/>
              Mrs.
              <img src="images/checkbox.png"/>
              Miss
              <xsl:choose>
                <xsl:when test="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Gender/GenderCode = 'Female'">
                  <img src="images/checkbox_checked.png"/>
                </xsl:when>
                <xsl:otherwise>
                  <img src="images/checkbox.png"/>
                </xsl:otherwise>
              </xsl:choose>
              Ms.
            </div>
            <div class="content"><xsl:value-of select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Name/LastName"/></div>
          </div>
          <div id="givenName" class="field">
            <div class="label">Given Name(s)</div>
            <div class="content">
              <xsl:value-of select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Name/FirstName"/>
              <xsl:for-each select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Name/MiddleName">
                <xsl:call-template name="noBreakSpace"/><xsl:value-of select="MiddleName"/>
              </xsl:for-each>
            </div>
          </div>
          <div id="maidenName" class="field">
            <div class="label">Previous Surname or Maiden Name (if applicable)</div>
            <div class="content">
              <xsl:for-each select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/AlternateName">
                <xsl:value-of select="LastName"/><xsl:call-template name="noBreakSpace"/>
              </xsl:for-each>
            </div>
          </div>
          <div id="asn" class="field">
            <div class="label">Alberta Student Number</div>
            <div class="content"><xsl:value-of select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/AgencyAssignedID"/></div>
          </div>
          <div id="dateOfBirth" class="field">
            <div class="label">Date of Birth (yyyy/mm/dd)</div>
            <div class="content">
              <xsl:call-template name="dateFormat">
                <xsl:with-param name="date" select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Birth/BirthDate"/>
              </xsl:call-template>
            </div>
          </div>
          <div id="email" class="field">
            <div class="label">E-mail Address</div>
            <div class="content">
              <xsl:call-template name="valueOrBlank">
                <xsl:with-param name="value" select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Email/EmailAddress"/>
              </xsl:call-template>
            </div>
          </div>
          <div id="telephoneNumber" class="field">
            <div class="label">Telephone Number</div>
            <div class="content">
              <xsl:call-template name="phoneNumberFormat">
                <xsl:with-param name="countryCode" select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Phone/CountryPrefixCode"/>
                <xsl:with-param name="areaCode" select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Phone/AreaCityCode"/>
                <xsl:with-param name="phoneNumber" select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Phone/PhoneNumber"/>
                <xsl:with-param name="extension" select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Phone/PhoneNumberExtension"/>
              </xsl:call-template>
            </div>
          </div>
          <div id="mailingAddress" class="field">
            <div class="label">Current Mailing Address</div>
            <div class="content">
              <xsl:call-template name="valueOrBlank">
                <xsl:with-param name="value" select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Address/AddressLine"/>
              </xsl:call-template>
            </div>
          </div>
          <div id="city" class="field">
            <div class="label">City/Town</div>
            <div class="content">
              <xsl:call-template name="valueOrBlank">
                <xsl:with-param name="value" select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Address/City"/>
              </xsl:call-template>
            </div>
          </div>
          <div id="province" class="field">
            <div class="label">Province</div>
            <div class="content">
              <xsl:choose>
                <xsl:when test="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Address/StateProvince">
                  <xsl:value-of select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Address/StateProvince"/>
                </xsl:when>
                <xsl:when test="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Address/StateProvinceCode">
                  <xsl:call-template name="stateProvinceLookup">
                    <xsl:with-param name="stateProvinceCode" select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Address/StateProvinceCode"/>
                  </xsl:call-template>
                </xsl:when>
                <xsl:otherwise> 
                  <xsl:call-template name="noBreakSpace"/>
                </xsl:otherwise>
              </xsl:choose>
            </div>
          </div>
          <div id="postalCode" class="field">
            <div class="label">Postal Code</div>
            <div class="content">
              <xsl:call-template name="valueOrBlank">
                <xsl:with-param name="value" select="tr:TranscriptRequest/tr:Request/RequestedStudent/Person/Contacts[1]/Address/PostalCode"/>
              </xsl:call-template>
            </div>
          </div>
          <div id="highSchool" class="field">
            <div class="label">Name of High School (either currently attending or most recently attended)</div>
            <div class="content">
              <xsl:call-template name="selectLatestAttendance">
                <xsl:with-param name="displayElement">name</xsl:with-param>
              </xsl:call-template>
            </div>
          </div>
          <div id="highSchoolCity" class="field">
            <div class="label">City/Town of High School</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="lastYearAttended" class="field">
            <div class="label">Last Year Attended</div>
            <div class="content">
              <xsl:call-template name="selectLatestAttendance">
                <xsl:with-param name="displayElement">date</xsl:with-param>
              </xsl:call-template>
            </div>
          </div>
          <div id="lastGradeAttended" class="field">
            <div class="label">Last Grade Attended</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
        </div>
        <div class="title">TRANSCRIPT INFORMATION</div>
        <div class="section">
          <div id="timeCodeHeader" class="field timeCode">
            <div class="label">Time Code</div>
          </div>
          <div id="numberOfCopiesHeader" class="field numberOfCopies">
            <div class="label">Number of Copies</div>
          </div>
          <div id="destinationCodeHeader" class="field destinationCode">
            <div class="label">Destination Code</div>
          </div>
          <div id="transcriptLanguageHeader" class="field transcriptLanguage">
            <div class="label">Transcript Language (E / F)</div>
          </div>
          <div id="destinationNameHeader" class="field destinationName">
            <div class="label">Name of Destination</div>
          </div>
          <div id="destinationAddressHeader" class="field destinationAddress">
            <div class="label">Full Address of Destination</div>
          </div>
          <div class="field timeCode">
            <div class="content">
              <xsl:choose>
                <xsl:when test="tr:TranscriptRequest/tr:Request/Recipient/TranscriptHold/SessionDesignator">
                  <xsl:call-template name="timeCodeLookup">
                    <xsl:with-param name="date" select="tr:TranscriptRequest/tr:Request/Recipient/TranscriptHold/SessionDesignator"/>
                  </xsl:call-template>
                </xsl:when>
                <xsl:when test="tr:TranscriptRequest/tr:Request/Recipient/TranscriptHold/ReleaseDate">
                  <xsl:call-template name="timeCodeLookup">
                    <xsl:with-param name="date" select="tr:TranscriptRequest/tr:Request/Recipient/TranscriptHold/ReleaseDate"/>
                  </xsl:call-template>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:call-template name="timeCodeLookup"/>
                </xsl:otherwise>
              </xsl:choose>
            </div>
          </div>
          <div class="field numberOfCopies">
            <div class="content">
              1
            </div>
          </div>
          <div class="field destinationCode">
            <div class="content">
              <xsl:call-template name="destinationCodeLookup">
                <xsl:with-param name="organizationCode" select="tr:TranscriptRequest/tr:TransmissionData/Source/Organization/LocalOrganizationID/LocalOrganizationIDCode"/>
              </xsl:call-template>
            </div>
          </div>
          <div class="field transcriptLanguage">
            <div class="content">
              E
            </div>
          </div>
          <div class="field destinationName">
            <div class="content">
              <xsl:value-of select="tr:TranscriptRequest/tr:TransmissionData/Source/Organization/OrganizationName"/>
            </div>
          </div>
          <div class="field destinationAddress">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field timeCode">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field numberOfCopies">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field destinationCode">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field transcriptLanguage">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field destinationName">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field destinationAddress">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field timeCode">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field numberOfCopies">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field destinationCode">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field transcriptLanguage">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field destinationName">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field destinationAddress">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field timeCode">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field numberOfCopies">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field destinationCode">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field transcriptLanguage">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field destinationName">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
          <div class="field destinationAddress">
            <div class="content">
              <xsl:call-template name="noBreakSpace"/>
            </div>
          </div>
        </div>
        <div class="title">TRANSCRIPT FEES</div>
        <div class="section">
          <div id="transcriptCount" class="field">
            <br/>
            I would like to order <u><xsl:text disable-output-escaping="yes">&amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp;</xsl:text></u> Official Transcripts (at $10.00 each) for a total
            cost of $<u><xsl:text disable-output-escaping="yes">&amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp;</xsl:text></u>.
            <br/>
            <br/>
          </div>
          <div id="paymentMethod" class="field">
            <div class="content"><b>PAYMENT METHOD</b></div>
          </div>
          <div id="cashDebit" class="field">
            <div class="label"><img src="images/checkbox.png"/> Cash/Debit<br/>(in person only)</div>
          </div>
          <div id="cheque" class="field">
            <div class="label"><img src="images/checkbox.png"/> Cheque/money order payable to:<br/>Minister of Finance. Do not post-date cheques.</div>
          </div>
          <div id="visa" class="field">
            <div class="label"><img src="images/checkbox.png"/> VISA</div>
          </div>
          <div id="masterCard" class="field">
            <div class="label"><img src="images/checkbox.png"/> MasterCard</div>
          </div>
          <div id="cardNumber" class="field">
            <div class="label">Card Number</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="expiryDate" class="field">
            <div class="label">Expiry Date</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="cardholder" class="field">
            <div class="label">Cardholder Name as printed on Credit Card</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="signature" class="field">
            <div class="label">Signature</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
        </div>
        <div class="title">STUDENT AUTHORIZATION</div>
        <div class="section">
          <div id="acknowledgement" class="field official">
            <div class="label">
              I acknowledge Alberta Education's authority to collect the general information contained on this form and
              authorize Alberta Education to disclose my transcript information to the destinations listed above in
              accordance with the instructions I have provided. I understand that this request will be processed only if
              signed by the student or an authorized person and appropriate fees remitted. Each request for a transcript
              fee refund will be subject to a $10.00 administrative fee.
            </div>
          </div>
          <div id="studentSignature" class="field">
            <div class="label">Student Signature</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="date" class="field">
            <div class="label">Date</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="relationship" class="field">
            <div class="label">
              If requesting on behalf of the student, please specify your relationship. I am the student's:
              <br/>
              <img src="images/checkbox.png"/> Parent (if student is 18 or over in age attach a letter of authorization)
              <br/>
              <img src="images/checkbox.png"/> Guardian (provide proof of guardianship and if student is 18 or over in age attach a letter of authorization)
            </div>
          </div>
          <div id="authorizationTelephoneNumber" class="field">
            <div class="label">Telephone Number</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="authorizationSurname" class="field">
            <div class="label">Surname</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="authorizationFirstName" class="field">
            <div class="label">First Name</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="authorizationMiddleName" class="field">
            <div class="label">Middle Name</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="authorizationSignature" class="field">
            <div class="label">Signature</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="authorizationDate" class="field">
            <div class="label">Date</div>
            <div class="content"><xsl:call-template name="noBreakSpace"/></div>
          </div>
          <div id="officeUseDriversLicence" class="field official">
            <div class="label">
              For Office Use Only
              <br/>
              <br/>
              <img src="images/checkbox.png"/> Driver's Licence
            </div>
          </div>
          <div id="studentBirthCertificate" class="field official">
            <div class="label">
              <img src="images/checkbox.png"/> Student
              <br/>
              <br/>
              <img src="images/checkbox.png"/> Birth Certificate
            </div>
          </div>
          <div id="passport" class="field official">
            <div class="label">
              <br/>
              <br/>
              <img src="images/checkbox.png"/> Passport
            </div>
          </div>
          <div id="parentGuardianVitalStatistics" class="field official">
            <div class="label">
              <img src="images/checkbox.png"/> Parent or Guardian
              <br/>
              <br/>
              <img src="images/checkbox.png"/> Vital Statistics
            </div>
          </div>
          <div id="other" class="field official">
            <div class="label">
              <br/>
              <br/>
              <img src="images/checkbox.png"/> Other <u><xsl:text disable-output-escaping="yes">&amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp;</xsl:text></u>
            </div>
          </div>
          <div id="initial" class="field official">
            <div class="label">
              <br/>
              <br/>
              <img src="images/checkbox.png"/> Initial <u><xsl:text disable-output-escaping="yes">&amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp;</xsl:text></u>
            </div>
          </div>
          <div id="initialDate" class="field official">
            <div class="label">
              <br/>
              <br/>
              <img src="images/checkbox.png"/> Date <u><xsl:text disable-output-escaping="yes">&amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp;</xsl:text></u>
            </div>
          </div>
          <div id="officeAddress" class="field official">
            <div class="label">
              <br/>
              <img src="images/checkbox.png"/> Address
            </div>
          </div>
          <div id="officePhone" class="field official">
            <div class="label">
              <br/>
              <img src="images/checkbox.png"/> Phone
            </div>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
  
  <xsl:template name="noBreakSpace">
    <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
  </xsl:template>
  
  <xsl:template name="valueOrBlank">
    <xsl:param name="value"/>
    <xsl:choose>
      <xsl:when test="$value">
        <xsl:value-of select="$value"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="noBreakSpace"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="dateFormat">
    <xsl:param name="date"/>
    <xsl:value-of select="substring($date, 1, 4)"/>/<xsl:value-of select="substring($date, 6, 2)"/>/<xsl:value-of select="substring($date, 9, 2)"/>
  </xsl:template>
  
  <xsl:template name="phoneNumberFormat">
    <xsl:param name="countryCode"/>
    <xsl:param name="areaCode"/>
    <xsl:param name="phoneNumber"/>
    <xsl:param name="extension"/>
    <xsl:if test="$countryCode">
      <xsl:value-of select="$countryCode"/>
    </xsl:if>
    <xsl:if test="$areaCode">
      (<xsl:value-of select="$areaCode"/>)
    </xsl:if>
    <xsl:if test="$phoneNumber">
      <xsl:value-of select="substring($phoneNumber, 1, 3)"/>-<xsl:value-of select="substring($phoneNumber, 4, 4)"/>
    </xsl:if>
    <xsl:if test="$extension">
      ext. <xsl:value-of select="$extension"/>
    </xsl:if>
    <xsl:call-template name="noBreakSpace"/>
  </xsl:template>
  
  <xsl:template name="stateProvinceLookup">
    <xsl:param name="stateProvinceCode"/>
    <xsl:choose>
      <xsl:when test="$stateProvinceCode = 'AA'">
        Military-Americas
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'AB'">
        Alberta
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'AE'">
        Military-Europe
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'AK'">
        Alaska
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'AL'">
        Alabama
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'AP'">
        Military-Pacific
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'AR'">
        Arkansas
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'AS'">
        American Samoa
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'AZ'">
        Arizona
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'BC'">
        British Columbia
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'CA'">
        California
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'CO'">
        Colorado
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'CT'">
        Connecticut
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'CZ'">
        Canal Zone
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'DC'">
        District Of Columbia
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'DE'">
        Delaware
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'FL'">
        Florida
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'FM'">
        Federated States Of Micronesia
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'GA'">
        Georgia
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'GU'">
        Guam
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'HI'">
        Hawaii
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'IA'">
        Iowa
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'ID'">
        Idaho
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'IL'">
        Illinois
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'IN'">
        Indiana
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'KS'">
        Kansas
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'KY'">
        Kentucky
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'LA'">
        Louisiana
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'MA'">
        Massachusetts
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'MB'">
        Manitoba
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'MD'">
        Maryland
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'ME'">
        Maine
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'MH'">
        Marshall Islands
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'MI'">
        Michigan
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'MN'">
        Minnesota
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'MO'">
        Missouri
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'MP'">
        Northern Mariana Islands
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'MS'">
        Mississippi
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'MT'">
        Montana
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NB'">
        New Brunswick
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NC'">
        North Carolina
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'ND'">
        North Dakota
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NE'">
        Nebraska
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NF'">
        Newfoundland
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NH'">
        New Hampshire
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NJ'">
        New Jersey
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NL'">
        Newfoundland And Labrador
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NM'">
        New Mexico
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NS'">
        Nova Scotia
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NT'">
        Northwest Territories
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NU'">
        Nunavut
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NV'">
        Nevada
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'NY'">
        New York
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'OH'">
        Ohio
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'OK'">
        Oklahoma
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'ON'">
        Ontario
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'OR'">
        Oregon
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'PA'">
        Pennsylvania
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'PE'">
        Prince Edward Island
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'PR'">
        Puerto Rico
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'PW'">
        Republic Of Palau
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'QC'">
        Quebec
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'RI'">
        Rhode Island
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'SC'">
        South Carolina
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'SD'">
        South Dakota
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'SK'">
        Saskatchewan
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'TN'">
        Tennessee
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'TX'">
        Texas
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'UT'">
        Utah
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'VA'">
        Virginia
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'VI'">
        Virgin Islands
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'VT'">
        Vermont
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'WA'">
        Washington
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'WI'">
        Wisconsin
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'WV'">
        West Virginia
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'WY'">
        Wyoming
      </xsl:when>
      <xsl:when test="$stateProvinceCode = 'YT'">
        Yukon
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="selectLatestAttendance">
    <xsl:param name="displayElement"/>
    <xsl:if test="count(tr:TranscriptRequest/tr:Request/RequestedStudent/Attendance) = 0">
      <xsl:call-template name="noBreakSpace"/>
    </xsl:if>
    <xsl:for-each select="tr:TranscriptRequest/tr:Request/RequestedStudent/Attendance">
      <xsl:sort select="ExitDate" order="descending"/>
      <xsl:if test="position() = 1">
        <xsl:choose>
          <xsl:when test="$displayElement = 'name'">
            <xsl:value-of select="/School/OrganizationName"/>
          </xsl:when>
          <xsl:when test="$displayElement = 'date'">
            <xsl:if test="ExitDate">
              <xsl:value-of select="substring(ExitDate, 1, 4)"/>
            </xsl:if>
            <xsl:if test="not(ExitDate)">
              <xsl:call-template name="noBreakSpace"/>
            </xsl:if>
          </xsl:when>
        </xsl:choose>
      </xsl:if>
    </xsl:for-each>
  </xsl:template>
  
  <xsl:template name="timeCodeLookup">
    <xsl:param name="date"/>
    <xsl:choose>
      <xsl:when test="$date">
        <xsl:variable name="currentYear" select="number(substring(tr:TranscriptRequest/tr:TransmissionData/CreatedDateTime, 1, 4))"/>
        <xsl:variable name="currentMonth" select="number(substring(tr:TranscriptRequest/tr:TransmissionData/CreatedDateTime, 6, 2))"/>
        <xsl:variable name="year" select="number(substring($date, 1, 4))"/>
        <xsl:variable name="month" select="number(substring($date, 6, 2))"/>
        <xsl:choose>
          <xsl:when test="($year = $currentYear and $month &gt;= $currentMonth) or ($year = $currentYear + 1 and $month &lt; $currentMonth)">
            <xsl:choose>
              <xsl:when test="$month = 1">
                1st
              </xsl:when>
              <xsl:when test="($month &gt; 1 and $month &lt;= 6)">
                2nd
              </xsl:when>
              <xsl:otherwise>
                SS
              </xsl:otherwise>
            </xsl:choose>
          </xsl:when>
          <xsl:when test="($year = $currentYear and $month &lt; $currentMonth)">
            ASAP
          </xsl:when>
          <xsl:otherwise>
            SS
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
      <xsl:otherwise>
        ASAP
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="destinationCodeLookup">
    <xsl:param name="organizationCode"/>
    <xsl:choose>
      <xsl:when test="$organizationCode = '48022000'">
        008
      </xsl:when>
      <xsl:when test="$organizationCode = '48026000'">
        014
      </xsl:when>
      <xsl:when test="$organizationCode = '48030000'">
        015
      </xsl:when>
      <xsl:when test="$organizationCode = '48031000'">
        016
      </xsl:when>
      <xsl:when test="$organizationCode = '48024000'">
        017
      </xsl:when>
      <xsl:when test="$organizationCode = '48027000'">
        018
      </xsl:when>
      <xsl:when test="$organizationCode = '48028000'">
        019
      </xsl:when>
      <xsl:when test="$organizationCode = '48029000'">
        020
      </xsl:when>
      <xsl:when test="$organizationCode = '48033000'">
        037
      </xsl:when>
      <xsl:when test="$organizationCode = '48146000'">
        007
      </xsl:when>
      <xsl:when test="$organizationCode = '48147000'">
        030
      </xsl:when>
      <xsl:when test="$organizationCode = '48023000'">
        022
      </xsl:when>
      <xsl:when test="$organizationCode = '48032000'">
        026
      </xsl:when>
      <xsl:when test="$organizationCode = '48034000'">
        027
      </xsl:when>
      <xsl:when test="$organizationCode = '48001000'">
        002
      </xsl:when>
      <xsl:when test="$organizationCode = '48005000'">
        003
      </xsl:when>
      <xsl:when test="$organizationCode = '48009000'">
        004
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="noBreakSpace"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
