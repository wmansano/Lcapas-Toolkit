<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:AdmApp="urn:ca:applyalberta:message:AdmissionApplication:v0.0.1" exclude-result-prefixes="AdmApp">
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>Admissions Application (Post Secondary)</title>
      </head>
      <body>
        <div class="main">
          <div class="container">
            <div id="headingLbl" class="labelRed">Admissions Application (Post Secondary)</div>
            <table class="AdmAppTbl">
              <tr class="topHeaderRow">
                <td colspan="5">
                  <div id="DataSheetLbl" class="headerLbl">DATA SHEET:</div>
                </td>
                <td colspan="1">
                  <div id="DateLbl" class="formLbl">DATE:</div>
                </td>
              </tr>
              <tr class="headerRow">
                <td colspan="6">
                  <div id="PerInfoLbl" class="headerLbl">PERSONAL INFORMATION:</div>       
                </td>
              </tr>
              <tr>
                <td colspan="5">
                  <div id="FullNameLbl" class="formLbl">NAME (Last, First Middle):</div> 
                  <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Name/LastName"/>, <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Name/FirstName"/> <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Name/MiddleName"/>
                </td>
                <td colspan="1">
                  <div id="LCIDLbl" class="formLbl">LC ID:</div>
                  
                </td>
              </tr>
              <tr class="headerRow">
                <td colspan="6">
                  <div id="FormerNamesLbl" class="headerLbl">FORMER NAMES:</div>
                  
                </td>
              </tr>
              <tr>
                <td colspan="3">
                  <div id="AddrLine1Lbl" class="formLbl">ADDR LN1:</div>
                  <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Contacts/PermanentAddress/AddressLine"/>
                </td>
                <td colspan="3">
                  <div id="AddrLine2Lbl" class="formLbl">ADDR LN 2:</div>
                </td>
              </tr>
              <tr>
                <td colspan="4">
                  <div id="CityProvPostalLbl" class="formLbl">CITY, PROV POSTAL:</div>
                  <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Contacts/PermanentAddress/City"/>,
                  <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Contacts/PermanentAddress/StateProvinceCode"/>, 
                  <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Contacts/PermanentAddress/PostalCode"/>
                </td>
                <td colspan="2">
                  <div id="CountryLbl" class="formLbl">COUNTRY:</div>
                  
                </td>
              </tr>
              <tr>
                <td colspan="3">
                  <div id="PhoneLbl" class="formLbl">PHONE:</div>
                  <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Contacts/PermanentPhone/CountryPrefixCode"/> (<xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Contacts/PermanentPhone/AreaCityCode"/>) <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Contacts/PermanentPhone/PhoneNumber"/>
                </td>
                <td colspan="3">
                  <div id="MobileLbl" class="formLbl">CELL PHONE:</div>
                </td>
              </tr>
              <tr>
                <td colspan="3">
                  <div id="GenderLbl" class="formLbl">GENDER:</div>
                  <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Gender/GenderCode"/>
                </td>
                <td colspan="3">
                  <div id="BirthLbl" class="formLbl">BIRTHDATE (d/M/y):</div>
                  <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Birth/BirthDate"/>
                </td>
              </tr>
              <tr>
                <td colspan="3">
                  <div id="AborStatLbl" class="formLbl">ABORIGINAL STATUS::</div>
                  <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/EthnicityRace/LocalRaceEthnicityCode"/>
                </td>
                <td colspan="3">
                  <div id="EmailLbl" class="formLbl">EMAIL:</div>
                  <xsl:value-of select="AdmApp:AdmissionApplication/AdmApp:Applicant/Person/Contacts/PrimaryEmail/EmailAddress"/>
                </td>
              </tr>
              <!-- Loop through applications-->
              <xsl:for-each select="AdmApp:AdmissionApplication/AdmApp:Applicant/Application">
                <tr class="headerRow">
                  <td colspan="6">
                    <div id="AppInfoLbl" class="headerLbl">APPLICATION INFORMATION:</div>
                  </td>
                </tr>
                <tr>
                  <td colspan="6">
                    <div id="StuTypeLbl" class="formLbl">TYPE OF STUDENT:</div>
                    <xsl:value-of select="ApplicationSource"/>
                  </td>
                </tr>
                <!-- Loop through application detail-->
                <xsl:for-each select="ApplicationDetail">
                  <tr>
                    <td colspan="6">
                      <div id="TermLbl" class="formLbl">TERM:</div>
                      <xsl:value-of select="ApplicationSession/SessionDesignator"/>
                    </td>
                  </tr>
                  <xsl:for-each select="ApplicationDegreeProgram">
                    <tr>
                      <td colspan="4">
                        <div id="ProgramLbl" class="formLbl">PROGRAM:</div>
                          <xsl:value-of select="AcademicProgramName"/>
                      </td>
                      <td colspan="2">
                        <div id="LocationLbl" class="formLbl">LOCATION:</div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="3">
                        <div id="PrevApplyLbl" class="formLbl">PREVIOUSLY APPLIED:</div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="3">
                        <div id="AdmitStatLbl" class="formLbl">ADMIT STATUS:</div>
                      </td>
                      <td colspan="3">
                        <div id="ResidencyLbl" class="formLbl">RESIDENCY:</div>
                        
                      </td>
                    </tr>
                  </xsl:for-each>
                </xsl:for-each>
                <tr class="headerRow">
                  <td colspan="6">
                    <div id="PayInfoLbl" class="headerLbl">PAYMENT INFORMATION:</div>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <div id="CardTypeLbl" class="formLbl">CARD TYPE:</div>
                  </td>
                  <td colspan="3">
                    <div id="ConfNumLbl" class="formLbl">CONFIRMATION #:</div>
                  </td>
                </tr>
                <tr>
                  <td colspan="6">
                    <div id="CardNameLbl" class="formLbl">NAME ON CARD:</div>
                  </td>
                </tr>
                <tr class="headerRow">
                  <td colspan="6">
                    <div id="AppInfoLbl" class="headerLbl">CITIZENSHIP INFORMATION:</div>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <div id="CitizenLbl" class="formLbl">CITIZENSHIP:</div>
                  </td>
                  <td colspan="2">
                    <div id="CountryLbl" class="formLbl">COUNTRY:</div>
                  </td>
                  <td colspan="2">
                    <div id="ResCountryLbl" class="formLbl">RESIDENCY COUNTRY:</div>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <div id="EntryDateLbl" class="formLbl">ENTRY DATE(d/M/y):</div>
                  </td>
                  <td colspan="3">
                    <div id="FirstLangLbl" class="formLbl">FIRST LANGUAGE:</div>
                  </td>
                </tr>
                <tr class="headerRow">
                  <td colspan="6">
                    <div id="OtherInfoLbl" class="headerLbl">OTHER INFORMATION:</div>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <div id="MarStatLbl" class="formLbl">MARITAL STATUS:</div>
                  </td>
                  <td colspan="3">
                    <div id="AbEdIdLbl" class="formLbl">AB ED ID:</div>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <div id="AltContactLbl" class="formLbl">ALTERNATE CONTACT:</div>
                  </td>
                  <td colspan="3">
                    <div id="AltContPhLbl" class="formLbl">ALT CONTACT PH:</div>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <div id="InflToApplyLbl" class="formLbl">INFLUENCED TO APPLY:</div>
                  </td>
                  <td colspan="3">
                    <div id="DisabilityLbl" class="formLbl">DISABILITY:</div>
                  </td>
                </tr>
              </xsl:for-each>
            </table>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
