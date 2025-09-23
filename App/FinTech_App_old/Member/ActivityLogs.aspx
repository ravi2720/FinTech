<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="ActivityLogs.aspx.cs" Inherits="Member_ActivityLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-container container-fluid">

				<!-- Row -->
				<div class="row">
					<div class="col-lg-12">
						<div class="card custom-card">
							<div class="card-header custom-card-header">
								<h6 class="card-title mb-0">Login Activity Logs</h6>
							</div>
							<div class="card-body">
								<div class="vtimeline">
                                    <asp:Repeater runat="server" ID="rptData">
                                        <ItemTemplate>
                                            	<div class="timeline-wrapper timeline-wrapper-primary">
										<div class="timeline-badge"></div>
										<div class="timeline-panel">
											<div class="timeline-heading">
												<h6 class="timeline-title"><%# Eval("IP") %></h6>
											</div>
											<div class="timeline-body">
												<p>System Login At <b><%# Eval("OS") %></b> with Browser <b><%# Eval("Browser") %></b></p>
											</div>
											<div class="timeline-footer d-flex align-items-center flex-wrap">
												<span class="ms-auto"><i class="fe fe-calendar text-muted me-1"></i><%# Eval("ADDDATE") %></span>
											</div>
										</div>
									</div>
                                        </ItemTemplate>
                                    </asp:Repeater>
								
								</div>
							</div>
						</div>
					</div>
				</div>
				<!-- End Row -->

			</div>
</asp:Content>

