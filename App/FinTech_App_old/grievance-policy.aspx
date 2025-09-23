<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="grievance-policy.aspx.cs" Inherits="services" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!-- services -->

    <section class="banner-bottom-w3layouts bg-li py-5 mt-5" id="services">

        <div class="container py-xl-5 py-lg-3">

            <h3 class="tittle text-center font-weight-bold">Introduction to Grievance Policy</h3>

            <p class="sub-tittle text-center mt-3 mb-sm-5 mb-4">
               This policy outlines a structured grievance redressal mechanism available to customers, regulators, and other parties to obtain a proposal for increasing their grievances. Our complaints policy is designed to resolve customer complaints in a timely and efficient manner while treating our customers justly and courteously. SoniTechno strives to provide best-in-class service delivery and has a dedicated customer service team. SoniTechno will short out with all customer complaints in a transparent and punctual manner.
            </p>   
            <div class="container ">
                <h4 class="title w-600 mb--50 mt--100 text-center" data-sal="slide-down" data-sal-duration="400"
                    data-sal-delay="350">Redressal mechanism for grievance raised over Email</h4>
                <div style="overflow-x:auto;">
                    <table align="center">

                        <tr align="center">
                            <th id="heading-table">Raise</th>
                            <th id="heading-table">Contact Details</th>
                            <th id="heading-table">Remarks</th>
                        </tr>
                        <tr>
                            <td align="center">Raise a Support Ticket.</td>
                            <td align="center">Customer Care<br><br>
                                Contact Email: <a href="#"><%=company.Email %></a></td>
                            <td>
                                ➤ <%=company.Name %> Customer Care team will acknowledge the <br>complaint with a ticket
                                number.<br><br>
                                ➤ We are committed to providing You with our first response <br>within 24 - 48 hours of
                                receiving the complaint.<br> <br>
                                ➤ Resolution of Your complaint may get delayed due to <br> operational or technical
                                reasons. In such a scenario, <br> <%=company.Name %> will inform the same to the customer.<br><br>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">Raise a Grievance</td>
                            <td align="center">Grievance Escalation Team <br> <br>
                                Contact Email: <a href="#"><%=company.AlternateEmail %></a></td>
                            <td>
                               ➤ In case of Level 1 resolution is not satisfactory,
                               <br>the customer can escalate the same to the <br>
                               Grievance Escalation Team.<br><br>
                               ➤ The ticket number of 1st level is mandatory.<br><br><br>
                               ➤ We are committed to providing You with our first <br>
                               response within 24 hours of receiving the complaint.<br><br>
                               ➤ In case of any unauthorized/fraudulent transaction <br>
                               reporting, the regulatory authority or customer can <br>
                               skip previous levels and direct mail to the
                               <br>Grievance Escalation Team.<br> <br>
                               ➤ Resolution within 5 working days except for national <br>holidays.<br> <br>
                               ➤ Resolution of Your complaint may get delayed due to <br>
                                operational or technical reasons. In such a scenario,<br>
                                Our Grievance Escalation Team will inform the <br> same to the customer.
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

    </section>

    <!-- //services -->
</asp:Content>

