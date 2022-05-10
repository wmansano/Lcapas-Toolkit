<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="html"/>

  <xsl:template match="/">
    <html>
      <head>
        <xsl:call-template name="htmlHead" />
      </head>
      <body style="font-size:13px; width:auto; ">
        <div class="main">
          <div class="hide-print right top-right-fixed">
            <a href="javascript:window.print()">
              <img id="print_img" src="Print.png" title="Print Document" />
            </a>
          </div>
          <div class="hide-print right top-right-fixed" style="margin-right:10px;">
            <a href="javascript:toggleHide()">
              <img id="page_img" src="Page.png" title="Print Back of Transcript" />
            </a>
          </div>
          <div class="container">
            <xsl:call-template name="documentHeader" />
            <xsl:call-template name="transcriptDetailsGrid" />
            <xsl:call-template name="Footer" />
          </div>
        </div>
      </body>
    </html>
  </xsl:template>


  <xsl:template name="htmlHead">
    <title>Lethbridge College Trancript</title>
    <script type="text/javascript">
      function toggleHide() {
      document.getElementById("last-page").classList.toggle("hide");
      }
    </script>
    <style type="text/css" media="print">
      .display { margin-top:0px; }
      .mainContainer {
      width:98%;
      max-width:840px;
      margin:0;
      float:none;
      padding-top:0px;
      padding-left:0px;
      }
      .hide-print { display: none; }
    </style>
    <style type="text/css">
      .container { margin-left: auto;  margin-right: auto;  width: 700px;  text-align: left;  }
      .main, .center { text-align: center;  padding: 5px;  }
      .hide { display: none; }
      .clear { clear:both; }
      .margin-top-0 { margin-top: 0; }


      hr {
      height:1px;
      border:inset 1px #000000;
      }
      hr.thick{
      height:1px;
      border:inset 2px #000000;
      }
      ul
      {
      list-style-type: none;
      }
      div.left {
      float: left;
      width: auto;
      margin-right:10px;
      overflow:visible;
      }
      div.right {
      float: right;
      width: auto;
      }
      div.middle {
      margin: 0px;
      text-align:center;
      height:100px;
      }
      td {
      overflow: visible;
      white-space: nowrap;
      }
      table {
      width:100%;
      }
      .separatorRow > td
      {
      border:2px solid Black;
      }
      .gradeRow{
      text-align:center;
      }
      .programRow
      {
      padding-left:30px;
      }
      .courseCodeCell
      {
      padding-left: 45px;
      }
      .unitTable td
      {
      border:1px solid Black;
      }
      .systemTable td
      {
      padding-left:11px;
      }
      .mainContainer
      {
      width:98%;
      max-width:1024px;
      margin:0;
      float:none;
      padding-top:0px;
      padding-left:0px;
      }
      #ProgramTermName
      {
      width:42%;
      }
      #classCode
      {
      width:18%;
      }
      #headAttempt
      {
      font-size:12px;
      width:5.5%;
      }
      #headEarned
      {
      font-size:12px;
      text-align:right;
      width:5.5%;
      }
      #headPoints
      {
      font-size:12px;
      width:5.5%;
      }
      #headGrade
      {
      font-size:12px;
      width:5.5%;
      }
      #headGPA
      {
      font-size:12px;
      width:5.5%;
      }
    </style>
  </xsl:template>

  <xsl:template name="documentHeader" match="Person">
    <div class="mainContainer">
      <table style="table-layout:fixed;">
        <thead>
          <tr>
            <th style="width:125px;"></th>
            <th style="width:120px;"></th>
            <th style="width:120px;"></th>
            <th style="width:105px;"></th>
            <th style="width:105px;"></th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>
              <img style="width:130px; padding-left:15px;" src="../Content/Images/lc_logo.png"  />
            </td>
            <td style="vertical-align:center; text-align:center;" colspan="2">
              <h1 id="header" style="padding-top:30px; font-size:16px;">Transcript of Academic Record</h1>
            </td>
            <td colspan="2">
              <div style="font-size:10px;">
                <div>
                  <span style="width:32%; display:inline-block; ">
                    <b>Tracking ID: </b>
                  </span>
                  <span style="width:66.5%; text-align:right; display:inline-block; overflow:hidden; vertical-align:bottom;">
                    <xsl:value-of select="//../../TransmissionData/RequestTrackingID"/>
                  </span>
                </div>
                <div>
                  <span style="width:32%; display:inline-block;">
                    <b>Document ID: </b>
                  </span>
                  <span style="width:66.5%; text-align:right; display:inline-block; overflow:hidden; vertical-align:bottom;">
                    <xsl:value-of select="//../../TransmissionData/DocumentID"/>
                  </span>
                </div>
                <div>
                  <span style="width:32%; display:inline-block;">
                    <b>Generated: </b>
                  </span>
                  <span style="width:66.5%; text-align:right; display:inline-block; overflow:hidden; vertical-align:bottom;">
                    <xsl:value-of select="concat(substring(//../../TransmissionData/CreatedDateTime, 9, 2), '-')"/>
                    <xsl:choose>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '01'">Jan</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '02'">Feb</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '03'">Mar</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '04'">Apr</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '05'">May</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '06'">Jun</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '07'">Jul</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '08'">Aug</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '09'">Sep</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '10'">Oct</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '11'">Nov</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '12'">Dec</xsl:when>
                    </xsl:choose>
                    <xsl:value-of select="concat('-', substring(//../../TransmissionData/CreatedDateTime, 1, 4))"/>
                  </span>
                </div>
              </div>
            </td>
          </tr>
          <tr style="height:28px;"></tr>
          <tr>
            <td colspan="2">
              <b>Student: </b>
              <xsl:value-of select="concat(//Name/FirstName,' ', //Name/MiddleName, ' ',//Name/LastName,' ')"/>
            </td>
            <td>
              <b>Student ID: </b>
              <xsl:value-of select="//SchoolAssignedPersonID"/>
            </td>
            <td>
              <b>Date of Birth: </b>
              <xsl:choose>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '01'">Jan</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '02'">Feb</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '03'">Mar</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '04'">Apr</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '05'">May</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '06'">Jun</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '07'">Jul</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '08'">Aug</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '09'">Sep</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '10'">Oct</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '11'">Nov</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '12'">Dec</xsl:when>
              </xsl:choose>
              -
              <xsl:value-of select="substring(//Birth/BirthDate, 9, 2)"/>
            </td>
            <td style="text-align:right;">
              <b>ASN: </b>
              <xsl:value-of select="//AgencyAssignedID"/>
            </td>
          </tr>
        </tbody>
      </table>
      <hr class="thick" />
    </div>
  </xsl:template>

  <xsl:template name="transcriptDetailsGrid">
    <div class="mainContainer">
      <xsl:call-template name="transferComments" />
      <div>
        <table style="table-layout:fixed;">
          <thead>
            <tr>
              <th id="classCode"></th>
              <th id="ProgramTermName"></th>
              <th id="headAttempt">Attempt</th>
              <th id="headEarned">Earned</th>
              <th id="headPoints">Points</th>
              <th id="headGrade">Grade</th>
              <th id="headGPA">GPA</th>
            </tr>
          </thead>
          <tbody>

            <!-- Get All sessions/terms -->
            <xsl:apply-templates select="//AcademicSession" />

          </tbody>
        </table>
      </div>
      <div>
        <xsl:apply-templates select="//AcademicRecord/AcademicAward" />
      </div>
      <hr class="thick" />
      <div style="text-align:center;">*** End of Transcript ***</div>
    </div>
  </xsl:template>

  <xsl:template name="Footer">
    <div id="last-page" style="page-break-before:always; margin-top:60px; margin-bottom:60px;">
      <div style="text-align:center;">
        <h1 style="margin-bottom:0px;">Lethbridge College</h1>
      </div>
      <div style="text-align:center;">
        <p>
          Lethbridge College is a public post secondary institution and was established in 157 under the statutes of the <br />Province of Alberta.  Lethbridge College is a member of the Association of Canadian Community Colleges (ACCC)
        </p>
      </div>
      <div style="text-align:center;">
        <h2 style="margin-top:0px;">Key to Grade Systems</h2>
      </div>
      <div>
        <div style="float:left;width:49%;">
          <h3 class="margin-top-0"> - GRADING SYSTEM EFFECTIVE JULY 1, 2003 - </h3>
          <table>
            <thead style="font-weight:bold;">
              <tr>
                <td>Grade</td>
                <td>Grade Point</td>
                <td>Explanation</td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>A+</td>
                <td>4.0</td>
                <td>EXCELLENT</td>
              </tr>
              <tr>
                <td>A</td>
                <td>4.0</td>
                <td>EXCELLENT</td>
              </tr>
              <tr>
                <td>A-</td>
                <td>3.7</td>
                <td>EXCELLENT</td>
              </tr>
              <tr>
                <td>B+</td>
                <td>3.3</td>
                <td>GOOD</td>
              </tr>
              <tr>
                <td>B</td>
                <td>3.0</td>
                <td>GOOD</td>
              </tr>
              <tr>
                <td>B-</td>
                <td>2.7</td>
                <td>GOOD</td>
              </tr>
              <tr>
                <td>C+</td>
                <td>2.3</td>
                <td>SATISFACTORY</td>
              </tr>
              <tr>
                <td>C</td>
                <td>2.0</td>
                <td>SATISFACTORY</td>
              </tr>
              <tr>
                <td>C-</td>
                <td>1.7</td>
                <td>SATISFACTORY</td>
              </tr>
              <tr>
                <td>D+</td>
                <td>1.3</td>
                <td>MINIMAL PASS</td>
              </tr>
              <tr>
                <td>D</td>
                <td>1.0</td>
                <td>MINIMAL PASS</td>
              </tr>
              <tr>
                <td>F</td>
                <td>0.0</td>
                <td>FAILURE</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div style="float:right;width:49%;">
          <table>
            <thead style="font-weight:bold;">
              <tr>
                <td>Symbol</td>
                <td>Grade Point</td>
                <td>Explanation</td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>AF</td>
                <td>0.0</td>
                <td>Administrative Fail</td>
              </tr>
              <tr>
                <td>AI</td>
                <td>Nil</td>
                <td>Administrative Incomplete</td>
              </tr>
              <tr>
                <td>AS</td>
                <td>Nil</td>
                <td>Advance Standing</td>
              </tr>
              <tr>
                <td>AUD</td>
                <td>Nil</td>
                <td>Audit</td>
              </tr>
              <tr>
                <td>CR</td>
                <td>Nil</td>
                <td>Course Requirement Satisfied</td>
              </tr>
              <tr>
                <td>DF</td>
                <td>Nil</td>
                <td>Deferred Exam</td>
              </tr>
              <tr>
                <td>I</td>
                <td>Nil</td>
                <td>Incomplete (Interim Grade)</td>
              </tr>
              <tr>
                <td>IP</td>
                <td>Nil</td>
                <td>In Progress</td>
              </tr>
              <tr>
                <td>NCR</td>
                <td>Nil</td>
                <td>Course Requirement Not Satisfied</td>
              </tr>
              <tr>
                <td>NS</td>
                <td>Nil</td>
                <td>No Show</td>
              </tr>
              <tr>
                <td>PLC</td>
                <td>Nil</td>
                <td>Prior Learning Credit</td>
              </tr>
              <tr>
                <td>RW</td>
                <td>0.0</td>
                <td>Required to Withdraw</td>
              </tr>
              <tr>
                <td>WC</td>
                <td>Nil</td>
                <td>Withdrawal for Cause</td>
              </tr>
              <tr>
                <td>W</td>
                <td>Nil</td>
                <td>Withdrawal (Student Initiated)</td>
              </tr>
              <tr>
                <td>TR</td>
                <td>Nil</td>
                <td>Transfer Credit</td>
              </tr>
            </tbody>
          </table>
        </div>
        <br class="clear" />
        <br />
        <div style="float:left;width:49%;">
          <h3> - GRADING SYSTEM EFFECTIVE JULY 1, 1993 - </h3>
          <table>
            <thead style="font-weight:bold;">
              <tr>
                <td>Grade</td>
                <td>Grade Point</td>
                <td>Explanation</td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>A+</td>
                <td>4.0</td>
                <td>EXCELLENT</td>
              </tr>
              <tr>
                <td>A</td>
                <td>4.0</td>
                <td>EXCELLENT</td>
              </tr>
              <tr>
                <td>B+</td>
                <td>3.3</td>
                <td>GOOD</td>
              </tr>
              <tr>
                <td>B</td>
                <td>3.0</td>
                <td>GOOD</td>
              </tr>
              <tr>
                <td>B-</td>
                <td>2.7</td>
                <td>GOOD</td>
              </tr>
              <tr>
                <td>C+</td>
                <td>2.3</td>
                <td>SATISFACTORY</td>
              </tr>
              <tr>
                <td>C</td>
                <td>2.0</td>
                <td>SATISFACTORY</td>
              </tr>
              <tr>
                <td>C-</td>
                <td>1.7</td>
                <td>SATISFACTORY</td>
              </tr>
              <tr>
                <td>D+</td>
                <td>1.3</td>
                <td>MINIMAL PASS</td>
              </tr>
              <tr>
                <td>D</td>
                <td>1.0</td>
                <td>MINIMAL PASS</td>
              </tr>
              <tr>
                <td>F</td>
                <td>0.0</td>
                <td>FAILURE</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div style="float:right;width:49%;">
          <h3> - GRADING SYSTEM PRIOR TO JULY 1, 1993 - </h3>
          <table>
            <thead style="font-weight:bold;">
              <tr>
                <td>Grade</td>
                <td>Grade Point</td>
                <td>Explanation</td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>A+</td>
                <td>4.0</td>
                <td>EXCELLENT</td>
              </tr>
              <tr>
                <td>A</td>
                <td>4.0</td>
                <td>EXCELLENT</td>
              </tr>
              <tr>
                <td>A-</td>
                <td>3.7</td>
                <td>EXCELLENT</td>
              </tr>
              <tr>
                <td>B+</td>
                <td>3.3</td>
                <td>GOOD</td>
              </tr>
              <tr>
                <td>B</td>
                <td>3.0</td>
                <td>GOOD</td>
              </tr>
              <tr>
                <td>B-</td>
                <td>2.7</td>
                <td>GOOD</td>
              </tr>
              <tr>
                <td>C+</td>
                <td>2.3</td>
                <td>SATISFACTORY</td>
              </tr>
              <tr>
                <td>C</td>
                <td>2.0</td>
                <td>SATISFACTORY</td>
              </tr>
              <tr>
                <td>C-</td>
                <td>1.7</td>
                <td>SATISFACTORY</td>
              </tr>
              <tr>
                <td>D+</td>
                <td>1.3</td>
                <td>MINIMAL PASS</td>
              </tr>
              <tr>
                <td>D</td>
                <td>1.0</td>
                <td>MINIMAL PASS</td>
              </tr>
              <tr>
                <td>F</td>
                <td>0.0</td>
                <td>FAILURE</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <div class="clear"></div>
      <div>
        <h3>Course Numbering</h3>
        <p>
          First year courses at the freshmen and junior level are generally numbered 100's and 200's and senior courses are numbered 300's and 400's.
        </p>
      </div>
      <div>
        <span style="width:32%; display:inline-block;">
          <h3>Course Weights</h3>
          <p>
            Course weights are indicated by credits. The credits are based on the type and quantity of instructional hours per semester and are used for calculating Grade Point Average (GPA). Prior to the 1999 summer semester, units were used as the weighting value for courses. The units to credit equivalencies are as follows:
          </p>
          <table class="unitTable" style="text-align:center;">
            <thead>
              <tr>
                <th>Units</th>
                <th>Credits</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>0.25</td>
                <td>1.0</td>
              </tr>
              <tr>
                <td>0.5</td>
                <td>1.5</td>
              </tr>
              <tr>
                <td>1.0</td>
                <td>3.0</td>
              </tr>
              <tr>
                <td>1.25</td>
                <td>4.0</td>
              </tr>
              <tr>
                <td>1.5</td>
                <td>4.5</td>
              </tr>
              <tr>
                <td>2.0</td>
                <td>6.0</td>
              </tr>
              <tr>
                <td>2.5</td>
                <td>7.5</td>
              </tr>
              <tr>
                <td>3.0</td>
                <td>9.0</td>
              </tr>
            </tbody>
          </table>
        </span>
        <span style="width:68%; display:inline-block; vertical-align:top;">
          <h3>Grading System</h3>
          <table class="systemTable">
            <thead>
              <tr>
                <th>Grade</th>
                <th>Grade Point Value</th>
                <th>Description</th>
                <th>Code</th>
                <th>Code Description</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>A+</td>
                <td>4.0</td>
                <td>Outstanding (effective July 2003)</td>
                <td>AU</td>
                <td>Audit</td>
              </tr>
              <tr>
                <td>A</td>
                <td>4.0</td>
                <td>Excellent</td>
                <td>CR</td>
                <td>Credit Awarded (Pass)</td>
              </tr>
              <tr>
                <td>A-</td>
                <td>3.7</td>
                <td></td>
                <td>IC</td>
                <td>Incomplete</td>
              </tr>
              <tr>
                <td>B+</td>
                <td>3.3</td>
                <td></td>
                <td>NC</td>
                <td>No Credit (Fail)</td>
              </tr>
              <tr>
                <td>B</td>
                <td>3.0</td>
                <td>Good</td>
                <td>RD</td>
                <td>Report Delayed</td>
              </tr>
              <tr>
                <td>B-</td>
                <td>2.7</td>
                <td></td>
                <td>TR</td>
                <td>Transfer Credit</td>
              </tr>
              <tr>
                <td>C+</td>
                <td>2.3</td>
                <td></td>
                <td>W</td>
                <td>Withdrawal</td>
              </tr>
              <tr>
                <td>C</td>
                <td>2.0</td>
                <td>Satisfactory</td>
                <td></td>
                <td></td>
              </tr>
              <tr>
                <td>C-</td>
                <td>1.7</td>
                <td>Minimum grade to proceed in same subject.</td>
                <td>**</td>
                <td>Re-taken Course</td>
              </tr>
              <tr>
                <td>D+</td>
                <td>1.3</td>
                <td></td>
                <td>R*</td>
                <td>Re-taken Override</td>
              </tr>
              <tr>
                <td>D</td>
                <td>1.0</td>
                <td>Minimal Pass/Marginal Performance</td>
                <td>*</td>
                <td>In Progress</td>
              </tr>
              <tr>
                <td>F</td>
                <td>0.0</td>
                <td>Fail</td>
                <td></td>
                <td></td>
              </tr>
            </tbody>
          </table>
        </span>
      </div>
      <div style="text-align:center; font-weight:bold;margin-top:30px">
        <p>
          Paper Transcript Records will be sent via Post
        </p>
      </div>
      <div style="margin-top:15px; text-align:center; font-weight:bold;">
        <p>
          <span style="font-size:18px;">Lethbridge College</span> <br />
          3000 College Drive South • Lethbridge • AB • T1K-1L6 • PH:(403) 320-3200 • Email: info@lethbridgecollege.ca<br />
        </p>
      </div>
    </div>
  </xsl:template>

  <xsl:template name="transferComments">
    <div>
      <xsl:for-each select="//AcademicRecord/NoteMessage">
        <xsl:value-of select="./text()"/>
      </xsl:for-each>
    </div>
  </xsl:template>

  <xsl:template  match="//AcademicRecord/AcademicAward">
    <hr />
    <div style="width:700px;">
      <p>
        <span style="font-weight:bold;">Credential Awarded: </span>
        <xsl:value-of select="./AcademicAwardTitle"/>
      </p>
      <p>
        <span style="font-weight:bold;">Date Awarded: </span>
        <xsl:value-of select="./AcademicAwardDate"/>
      </p>
      <xsl:if test="./NoteMessage" >
        <p>
          <span style="font-weight:bold;">Award Comments: </span>
          <xsl:value-of select="./NoteMessage"/>
        </p>
      </xsl:if>
    </div>
    <hr />
  </xsl:template>

  <xsl:template match="//AcademicSession" >
    <xsl:variable name="currentSessionBeginDate" select="translate(./AcademicSessionDetail/SessionBeginDate, '- ', '')" ></xsl:variable>
    <tr>
      <td>
        <b>
          <xsl:value-of select="./AcademicSessionDetail/SessionName"/>
        </b>
      </td>
    </tr>

    <!-- For each program, print this terms grades and summaries -->

    <xsl:for-each select="./AcademicProgram" >
      <xsl:variable name="programName" select="./AcademicProgramName" />
      <tr>
        <td class="programRow" colspan="6">
          <b>
            <xsl:value-of select="$programName" />
          </b>
        </td>
      </tr>

      <xsl:choose>
        <xsl:when test="../AcademicSessionDetail[SessionName = 'Transfer Credits'] and ../Course/NoteMessage[. != $programName]">
          <xsl:for-each select="../Course/NoteMessage[. != $programName and not(.=following::NoteMessage)]">
            <xsl:variable name="CurrentInstitition" select="./text()" />
            <tr>
              <td colspan="6" style="padding-left:40px;font-weight:bold; font-size:11px;">
                <xsl:value-of select="$CurrentInstitition"/>
              </td>
            </tr>
            <xsl:for-each select="../../Course[NoteMessage/text() = $CurrentInstitition]">
              <tr>
                <td  class="courseCodeCell" >
                  <xsl:value-of select="./CourseSubjectAbbreviation"/>
                  <xsl:value-of select="./CourseNumber"/>
                </td>
                <td>
                  <xsl:value-of select="./CourseTitle"/>
                </td>
                <td class="gradeRow">
                  <xsl:value-of select="./CourseCreditValue"/>
                </td>
                <td class="gradeRow">
                  <xsl:value-of select="./CourseCreditEarned"/>
                </td>
                <td class="gradeRow">
                  <xsl:value-of select="./CourseQualityPointsEarned"/>
                </td>
                <td  class="gradeRow" style="text-align:left; padding-left:15px;">
                  <xsl:value-of select="./CourseAcademicGrade"/>
                </td>
                <td  class="gradeRow">
                  <xsl:if test="./CourseRepeatCode = 'ReplacementCounted'" >
                    <span>**</span>
                  </xsl:if>
                </td>
              </tr>
            </xsl:for-each>
          </xsl:for-each>
        </xsl:when>
        <xsl:otherwise>
          <xsl:for-each select="../Course[NoteMessage = $programName]">
            <tr>
              <td  class="courseCodeCell" >
                <xsl:value-of select="./CourseSubjectAbbreviation"/>
                <xsl:value-of select="./CourseNumber"/>
              </td>
              <td>
                <xsl:value-of select="./CourseTitle"/>
              </td>
              <td class="gradeRow">
                <xsl:value-of select="./CourseCreditValue"/>
              </td>
              <td class="gradeRow">
                <xsl:value-of select="./CourseCreditEarned"/>
              </td>
              <td class="gradeRow">
                <xsl:value-of select="./CourseQualityPointsEarned"/>
              </td>
              <td  class="gradeRow" style="text-align:left; padding-left:15px;">
                <xsl:choose>
                  <xsl:when test="./CourseAcademicGrade">
                    <xsl:value-of select="./CourseAcademicGrade"/>
                  </xsl:when>
                  <xsl:otherwise>
                    *
                  </xsl:otherwise>
                </xsl:choose>
              </td>
              <td  class="gradeRow">
                <xsl:if test="./CourseRepeatCode = 'ReplacementCounted'" >
                  <span>**</span>
                </xsl:if>
              </td>
            </tr>
          </xsl:for-each>
        </xsl:otherwise>
      </xsl:choose>

      <!-- Determine whether student is on academic probation for this term -->

      <xsl:if test="../AcademicSummary/Delinquencies = 'ProbationGPA'">
        <tr style="height:30px; ">
          <td class="programRow">
            <b>
              Academic Probation
            </b>
          </td>
        </tr>
      </xsl:if>
      <xsl:if test="../AcademicSummary/AcademicHonors" >
        <tr style="height:30px; ">
          <td class="programRow">
            <b>
              <xsl:value-of select="../AcademicSummary/AcademicHonors/HonorsTitle" />
            </b>
          </td>
        </tr>
      </xsl:if>

      <!-- Total the grades only for this term -->

      <xsl:if test="../Course/CourseAcademicGrade" >   
        <xsl:if test="count(../AcademicProgram) = 1" >
          <tr style="height:15px; font-weight:bold; vertical-align:bottom;">
            <td style="text-align:right; font-size:12px;" colspan="2">Term Totals:</td>
            <td class="gradeRow">
              <xsl:value-of select="format-number(sum(//Course/CourseCreditValue[../NoteMessage = $programName and translate(../../AcademicSessionDetail/SessionBeginDate, '- ', '') = $currentSessionBeginDate and translate(../CourseAcademicGrade,'TR','') != '']), '0.00')"/>
            </td>
            <td class="gradeRow">
              <xsl:value-of select="format-number(sum(//Course/CourseCreditEarned[../NoteMessage = $programName and translate(../../AcademicSessionDetail/SessionBeginDate, '- ', '') = $currentSessionBeginDate and translate(../CourseAcademicGrade,'TR','') != '']), '0.00')"/>
            </td>
            <td class="gradeRow">
              <xsl:value-of select="format-number(sum(//Course/CourseQualityPointsEarned[../NoteMessage = $programName and translate(../../AcademicSessionDetail/SessionBeginDate, '- ', '') = $currentSessionBeginDate and translate(../CourseAcademicGrade,'TR','') != '']), '0.00')"/>
            </td>
            <td  class="gradeRow">
            </td>
            <td  class="gradeRow">
              <!-- 
                      Calculation of GPA moved to database script
                      <xsl:value-of select="format-number(sum(//Course/CourseQualityPointsEarned[../NoteMessage = $programName and translate(../../AcademicSessionDetail/SessionBeginDate, '- ', '') = $currentSessionBeginDate and 
                          (contains(../CourseAcademicGrade, 'A') or contains(../CourseAcademicGrade, 'B') or contains(../CourseAcademicGrade, 'C') or contains(../CourseAcademicGrade, 'D') or contains(../CourseAcademicGrade, 'F'))]) 
                                  div (sum(//Course/CourseCreditValue[../NoteMessage = $programName and translate(../../AcademicSessionDetail/SessionBeginDate, '- ', '') = $currentSessionBeginDate and 
                          (contains(../CourseAcademicGrade, 'A') or contains(../CourseAcademicGrade, 'B') or contains(../CourseAcademicGrade, 'C') or contains(../CourseAcademicGrade, 'D') or contains(../CourseAcademicGrade, 'F'))])), '0.00')"/> -->
              <xsl:value-of select="format-number(../AcademicSummary/GPA/CreditHoursforGPA[../../NoteMessage = $programName], '0.00')"/>
            </td>
          </tr>
        </xsl:if>
        <tr style=" font-weight:bold; vertical-align:bottom;">
          <td style="text-align:right; font-size:12px;" colspan="2">
            Cum. <xsl:value-of select="$programName" /> Totals:
          </td>
          <td class="gradeRow">
            <xsl:value-of select="format-number(../AcademicProgram/NoteMessage[../AcademicProgramName = $programName and translate(../../AcademicSessionDetail/SessionBeginDate, '- ', '') &lt;= $currentSessionBeginDate][1], '0.00')"/>
          </td>
          <td class="gradeRow">
            <xsl:value-of select="format-number(../AcademicProgram/NoteMessage[../AcademicProgramName = $programName and translate(../../AcademicSessionDetail/SessionBeginDate, '- ', '') &lt;= $currentSessionBeginDate][2], '0.00')"/>
          </td>
          <td class="gradeRow">
            <xsl:value-of select="format-number(../AcademicProgram/NoteMessage[../AcademicProgramName = $programName and translate(../../AcademicSessionDetail/SessionBeginDate, '- ', '') &lt;= $currentSessionBeginDate][3], '0.00')"/>
          </td>
          <td  class="gradeRow">
          </td>
          <td  class="gradeRow">
            <xsl:value-of select="format-number(../AcademicSummary/GPA/GradePointAverage[../../NoteMessage = $programName], '0.00')"/>
          </td>
        </tr>
      </xsl:if>
    </xsl:for-each>
    <tr style="height:20px;">
      <td colspan="7">
        <hr />
      </td>
    </tr>

  </xsl:template>

</xsl:stylesheet>