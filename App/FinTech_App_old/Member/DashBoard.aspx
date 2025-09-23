<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="Member_DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .marquee {
            position: relative;
            animation: marquee 2s linear infinite;
            text-align: center;
            color: #ffffff;
        }

        @keyframes marquee {
            0% {
                top: 10em
            }

            100% {
                top: -2em
            }
        }


        svg .stroke {
            stroke: #06D;
            stroke-dasharray: 100%;
            stroke-dashoffset: -100%;
            animation: stroke 2.5s ease-in-out;
            animation-fill-mode: forwards;
        }

        svg .text {
            opacity: 0;
            animation: fade .75s 2.5s ease-in-out;
            animation-fill-mode: forwards;
            fill: #06D
        }

        @keyframes stroke {
            to {
                stroke-dashoffset: 0%;
            }
        }

        @keyframes fade {
            to {
                opacity: 1;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- main-content -->

    <!-- container -->
    <div class="main-container container-fluid" id="app1">
        <!-- main-content-body -->
        <div class="main-content-body">
            <div class="row row-sm" v-for="post in MainData">
                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12">
                    <div class="card overflow-hidden project-card">
                        <div class="card-body">
                            <a href="MoneyTransfer.aspx">
                                <div class="d-flex">
                                    <div class="my-auto">
                                        <svg enable-background="new 0 0 469.682 469.682" version="1.1" class="me-4 ht-60 wd-60 my-auto primary" viewBox="0 0 469.68 469.68" xml:space="preserve" xmlns="http://www.w3.org/2000/svg">
                                            <path d="m120.41 298.32h87.771c5.771 0 10.449-4.678 10.449-10.449s-4.678-10.449-10.449-10.449h-87.771c-5.771 0-10.449 4.678-10.449 10.449s4.678 10.449 10.449 10.449z" />
                                            <path d="m291.77 319.22h-171.36c-5.771 0-10.449 4.678-10.449 10.449s4.678 10.449 10.449 10.449h171.36c5.771 0 10.449-4.678 10.449-10.449s-4.678-10.449-10.449-10.449z" />
                                            <path d="m291.77 361.01h-171.36c-5.771 0-10.449 4.678-10.449 10.449s4.678 10.449 10.449 10.449h171.36c5.771 0 10.449-4.678 10.449-10.449s-4.678-10.449-10.449-10.449z" />
                                            <path d="m420.29 387.14v-344.82c0-22.987-16.196-42.318-39.183-42.318h-224.65c-22.988 0-44.408 19.331-44.408 42.318v20.376h-18.286c-22.988 0-44.408 17.763-44.408 40.751v345.34c0.68 6.37 4.644 11.919 10.449 14.629 6.009 2.654 13.026 1.416 17.763-3.135l31.869-28.735 38.139 33.959c2.845 2.639 6.569 4.128 10.449 4.18 3.861-0.144 7.554-1.621 10.449-4.18l37.616-33.959 37.616 33.959c5.95 5.322 14.948 5.322 20.898 0l38.139-33.959 31.347 28.735c3.795 4.671 10.374 5.987 15.673 3.135 5.191-2.98 8.232-8.656 7.837-14.629v-74.188l6.269-4.702 31.869 28.735c2.947 2.811 6.901 4.318 10.971 4.18 1.806 0.163 3.62-0.2 5.224-1.045 5.493-2.735 8.793-8.511 8.361-14.629zm-83.591 50.155-24.555-24.033c-5.533-5.656-14.56-5.887-20.376-0.522l-38.139 33.959-37.094-33.959c-6.108-4.89-14.79-4.89-20.898 0l-37.616 33.959-38.139-33.959c-6.589-5.4-16.134-5.178-22.465 0.522l-27.167 24.033v-333.84c0-11.494 12.016-19.853 23.51-19.853h224.65c11.494 0 18.286 8.359 18.286 19.853v333.84zm62.693-61.649-26.122-24.033c-4.18-4.18-5.224-5.224-15.673-3.657v-244.51c1.157-21.321-15.19-39.542-36.51-40.699-0.89-0.048-1.782-0.066-2.673-0.052h-185.47v-20.376c0-11.494 12.016-21.42 23.51-21.42h224.65c11.494 0 18.286 9.927 18.286 21.42v333.32z" />
                                            <path d="m232.21 104.49h-57.47c-11.542 0-20.898 9.356-20.898 20.898v104.49c0 11.542 9.356 20.898 20.898 20.898h57.469c11.542 0 20.898-9.356 20.898-20.898v-104.49c1e-3 -11.542-9.356-20.898-20.897-20.898zm0 123.3h-57.47v-13.584h57.469v13.584zm0-34.482h-57.47v-67.918h57.469v67.918z" />
                                        </svg>
                                    </div>
                                    <div class="project-content">
                                        <h6>Money Transfer</h6>
                                        <ul>
                                            <li>
                                                <strong>Processing</strong>
                                                <span>0</span>
                                            </li>

                                            <li>
                                                <strong>Paid</strong>
                                                <span>{{post.DMT}}</span>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12">
                    <div class="card overflow-hidden project-card">
                        <div class="card-body">
                            <a href="Prepaid.aspx">
                                <div class="d-flex">
                                    <div class="my-auto">
                                        <svg class="me-4 ht-60 wd-60 my-auto primary" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                            <rect x="6" y="2" width="12" height="20" rx="2" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                                            <path d="M11.95 18H12.05" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                                        </svg>
                                    </div>
                                    <div class="project-content">
                                        <h6>Recharge</h6>
                                        <ul>
                                            <li>
                                                <strong>Processing</strong>
                                                <span>0</span>
                                            </li>

                                            <li>
                                                <strong>Paid</strong>
                                                <span>{{post.Recharge}}</span>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12">
                    <div class="card overflow-hidden project-card">
                        <div class="card-body">
                            <a href="IntantPayActivation.aspx">
                                <div class="d-flex">
                                    <div class="my-auto">
                                        <svg xmlns="http://www.w3.org/2000/svg" class="me-4 ht-60 wd-60 my-auto primary" fill="currentColor" class="bi bi-fingerprint" viewBox="0 0 16 16">
                                            <path d="M8.06 6.5a.5.5 0 0 1 .5.5v.776a11.5 11.5 0 0 1-.552 3.519l-1.331 4.14a.5.5 0 0 1-.952-.305l1.33-4.141a10.5 10.5 0 0 0 .504-3.213V7a.5.5 0 0 1 .5-.5Z" />
                                            <path d="M6.06 7a2 2 0 1 1 4 0 .5.5 0 1 1-1 0 1 1 0 1 0-2 0v.332c0 .409-.022.816-.066 1.221A.5.5 0 0 1 6 8.447c.04-.37.06-.742.06-1.115V7Zm3.509 1a.5.5 0 0 1 .487.513 11.5 11.5 0 0 1-.587 3.339l-1.266 3.8a.5.5 0 0 1-.949-.317l1.267-3.8a10.5 10.5 0 0 0 .535-3.048A.5.5 0 0 1 9.569 8Zm-3.356 2.115a.5.5 0 0 1 .33.626L5.24 14.939a.5.5 0 1 1-.955-.296l1.303-4.199a.5.5 0 0 1 .625-.329Z" />
                                            <path d="M4.759 5.833A3.501 3.501 0 0 1 11.559 7a.5.5 0 0 1-1 0 2.5 2.5 0 0 0-4.857-.833.5.5 0 1 1-.943-.334Zm.3 1.67a.5.5 0 0 1 .449.546 10.72 10.72 0 0 1-.4 2.031l-1.222 4.072a.5.5 0 1 1-.958-.287L4.15 9.793a9.72 9.72 0 0 0 .363-1.842.5.5 0 0 1 .546-.449Zm6 .647a.5.5 0 0 1 .5.5c0 1.28-.213 2.552-.632 3.762l-1.09 3.145a.5.5 0 0 1-.944-.327l1.089-3.145c.382-1.105.578-2.266.578-3.435a.5.5 0 0 1 .5-.5Z" />
                                            <path d="M3.902 4.222a4.996 4.996 0 0 1 5.202-2.113.5.5 0 0 1-.208.979 3.996 3.996 0 0 0-4.163 1.69.5.5 0 0 1-.831-.556Zm6.72-.955a.5.5 0 0 1 .705-.052A4.99 4.99 0 0 1 13.059 7v1.5a.5.5 0 1 1-1 0V7a3.99 3.99 0 0 0-1.386-3.028.5.5 0 0 1-.051-.705ZM3.68 5.842a.5.5 0 0 1 .422.568c-.029.192-.044.39-.044.59 0 .71-.1 1.417-.298 2.1l-1.14 3.923a.5.5 0 1 1-.96-.279L2.8 8.821A6.531 6.531 0 0 0 3.058 7c0-.25.019-.496.054-.736a.5.5 0 0 1 .568-.422Zm8.882 3.66a.5.5 0 0 1 .456.54c-.084 1-.298 1.986-.64 2.934l-.744 2.068a.5.5 0 0 1-.941-.338l.745-2.07a10.51 10.51 0 0 0 .584-2.678.5.5 0 0 1 .54-.456Z" />
                                            <path d="M4.81 1.37A6.5 6.5 0 0 1 14.56 7a.5.5 0 1 1-1 0 5.5 5.5 0 0 0-8.25-4.765.5.5 0 0 1-.5-.865Zm-.89 1.257a.5.5 0 0 1 .04.706A5.478 5.478 0 0 0 2.56 7a.5.5 0 0 1-1 0c0-1.664.626-3.184 1.655-4.333a.5.5 0 0 1 .706-.04ZM1.915 8.02a.5.5 0 0 1 .346.616l-.779 2.767a.5.5 0 1 1-.962-.27l.778-2.767a.5.5 0 0 1 .617-.346Zm12.15.481a.5.5 0 0 1 .49.51c-.03 1.499-.161 3.025-.727 4.533l-.07.187a.5.5 0 0 1-.936-.351l.07-.187c.506-1.35.634-2.74.663-4.202a.5.5 0 0 1 .51-.49Z" />
                                        </svg>
                                    </div>
                                    <div class="project-content">
                                        <h6>AePS</h6>
                                        <ul>
                                            <li>
                                                <strong>Processing</strong>
                                                <span>0</span>
                                            </li>

                                            <li>
                                                <strong>Paid</strong>
                                                <span>{{post.AEPS}}</span>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12">
                    <div class="card overflow-hidden project-card">
                        <div class="card-body">
                            <a href="Settlement.aspx">
                                <div class="d-flex">
                                    <div class="my-auto">
                                        <svg enable-background="new 0 0 469.682 469.682" version="1.1" class="me-4 ht-60 wd-60 my-auto primary" viewBox="0 0 469.68 469.68" xml:space="preserve" xmlns="http://www.w3.org/2000/svg">
                                            <path d="m120.41 298.32h87.771c5.771 0 10.449-4.678 10.449-10.449s-4.678-10.449-10.449-10.449h-87.771c-5.771 0-10.449 4.678-10.449 10.449s4.678 10.449 10.449 10.449z" />
                                            <path d="m291.77 319.22h-171.36c-5.771 0-10.449 4.678-10.449 10.449s4.678 10.449 10.449 10.449h171.36c5.771 0 10.449-4.678 10.449-10.449s-4.678-10.449-10.449-10.449z" />
                                            <path d="m291.77 361.01h-171.36c-5.771 0-10.449 4.678-10.449 10.449s4.678 10.449 10.449 10.449h171.36c5.771 0 10.449-4.678 10.449-10.449s-4.678-10.449-10.449-10.449z" />
                                            <path d="m420.29 387.14v-344.82c0-22.987-16.196-42.318-39.183-42.318h-224.65c-22.988 0-44.408 19.331-44.408 42.318v20.376h-18.286c-22.988 0-44.408 17.763-44.408 40.751v345.34c0.68 6.37 4.644 11.919 10.449 14.629 6.009 2.654 13.026 1.416 17.763-3.135l31.869-28.735 38.139 33.959c2.845 2.639 6.569 4.128 10.449 4.18 3.861-0.144 7.554-1.621 10.449-4.18l37.616-33.959 37.616 33.959c5.95 5.322 14.948 5.322 20.898 0l38.139-33.959 31.347 28.735c3.795 4.671 10.374 5.987 15.673 3.135 5.191-2.98 8.232-8.656 7.837-14.629v-74.188l6.269-4.702 31.869 28.735c2.947 2.811 6.901 4.318 10.971 4.18 1.806 0.163 3.62-0.2 5.224-1.045 5.493-2.735 8.793-8.511 8.361-14.629zm-83.591 50.155-24.555-24.033c-5.533-5.656-14.56-5.887-20.376-0.522l-38.139 33.959-37.094-33.959c-6.108-4.89-14.79-4.89-20.898 0l-37.616 33.959-38.139-33.959c-6.589-5.4-16.134-5.178-22.465 0.522l-27.167 24.033v-333.84c0-11.494 12.016-19.853 23.51-19.853h224.65c11.494 0 18.286 8.359 18.286 19.853v333.84zm62.693-61.649-26.122-24.033c-4.18-4.18-5.224-5.224-15.673-3.657v-244.51c1.157-21.321-15.19-39.542-36.51-40.699-0.89-0.048-1.782-0.066-2.673-0.052h-185.47v-20.376c0-11.494 12.016-21.42 23.51-21.42h224.65c11.494 0 18.286 9.927 18.286 21.42v333.32z" />
                                            <path d="m232.21 104.49h-57.47c-11.542 0-20.898 9.356-20.898 20.898v104.49c0 11.542 9.356 20.898 20.898 20.898h57.469c11.542 0 20.898-9.356 20.898-20.898v-104.49c1e-3 -11.542-9.356-20.898-20.897-20.898zm0 123.3h-57.47v-13.584h57.469v13.584zm0-34.482h-57.47v-67.918h57.469v67.918z" />
                                        </svg>
                                    </div>
                                    <div class="project-content">
                                        <h6>AePS Settlement</h6>
                                        <ul>
                                            <li>
                                                <strong>Processing</strong>
                                                <span>0</span>
                                            </li>

                                            <li>
                                                <strong>Paid</strong>
                                                <span>{{post.Payout}}</span>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
             <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12">
                <div class="card overflow-hidden project-card">
                    <div class="card-body">	                       				    
                            <div class="d-flex">
                                <div class="my-auto">
                                    <svg enable-background="new 0 0 469.682 469.682" version="1.1" class="me-4 ht-60 wd-60 my-auto primary" viewBox="0 0 469.68 469.68" xml:space="preserve" xmlns="http://www.w3.org/2000/svg">
                                        <path d="m120.41 298.32h87.771c5.771 0 10.449-4.678 10.449-10.449s-4.678-10.449-10.449-10.449h-87.771c-5.771 0-10.449 4.678-10.449 10.449s4.678 10.449 10.449 10.449z" />
                                        <path d="m291.77 319.22h-171.36c-5.771 0-10.449 4.678-10.449 10.449s4.678 10.449 10.449 10.449h171.36c5.771 0 10.449-4.678 10.449-10.449s-4.678-10.449-10.449-10.449z" />
                                        <path d="m291.77 361.01h-171.36c-5.771 0-10.449 4.678-10.449 10.449s4.678 10.449 10.449 10.449h171.36c5.771 0 10.449-4.678 10.449-10.449s-4.678-10.449-10.449-10.449z" />
                                        <path d="m420.29 387.14v-344.82c0-22.987-16.196-42.318-39.183-42.318h-224.65c-22.988 0-44.408 19.331-44.408 42.318v20.376h-18.286c-22.988 0-44.408 17.763-44.408 40.751v345.34c0.68 6.37 4.644 11.919 10.449 14.629 6.009 2.654 13.026 1.416 17.763-3.135l31.869-28.735 38.139 33.959c2.845 2.639 6.569 4.128 10.449 4.18 3.861-0.144 7.554-1.621 10.449-4.18l37.616-33.959 37.616 33.959c5.95 5.322 14.948 5.322 20.898 0l38.139-33.959 31.347 28.735c3.795 4.671 10.374 5.987 15.673 3.135 5.191-2.98 8.232-8.656 7.837-14.629v-74.188l6.269-4.702 31.869 28.735c2.947 2.811 6.901 4.318 10.971 4.18 1.806 0.163 3.62-0.2 5.224-1.045 5.493-2.735 8.793-8.511 8.361-14.629zm-83.591 50.155-24.555-24.033c-5.533-5.656-14.56-5.887-20.376-0.522l-38.139 33.959-37.094-33.959c-6.108-4.89-14.79-4.89-20.898 0l-37.616 33.959-38.139-33.959c-6.589-5.4-16.134-5.178-22.465 0.522l-27.167 24.033v-333.84c0-11.494 12.016-19.853 23.51-19.853h224.65c11.494 0 18.286 8.359 18.286 19.853v333.84zm62.693-61.649-26.122-24.033c-4.18-4.18-5.224-5.224-15.673-3.657v-244.51c1.157-21.321-15.19-39.542-36.51-40.699-0.89-0.048-1.782-0.066-2.673-0.052h-185.47v-20.376c0-11.494 12.016-21.42 23.51-21.42h224.65c11.494 0 18.286 9.927 18.286 21.42v333.32z" />
                                        <path d="m232.21 104.49h-57.47c-11.542 0-20.898 9.356-20.898 20.898v104.49c0 11.542 9.356 20.898 20.898 20.898h57.469c11.542 0 20.898-9.356 20.898-20.898v-104.49c1e-3 -11.542-9.356-20.898-20.897-20.898zm0 123.3h-57.47v-13.584h57.469v13.584zm0-34.482h-57.47v-67.918h57.469v67.918z" />
                                    </svg>
                                </div>
                                <div class="project-content">
                                    <h6>Micro ATM</h6>
                                    <ul>
                                        <li>
                                            <strong>Processing</strong>
                                            <span>0</span>
                                        </li>

                                        <li>
                                            <strong>Paid</strong>
                                            <span>{{post.MATM}}</span>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12">
                    <div class="card  overflow-hidden project-card">
                        <div class="card-body">
                            <a href="aepsbank.aspx">
                                <div class="d-flex">
                                    <div class="my-auto">
                                        <svg xmlns="http://www.w3.org/2000/svg" class="me-4 ht-60 wd-60 my-auto primary" fill="currentColor" class="bi bi-fingerprint" viewBox="0 0 16 16">
                                            <path d="M8.06 6.5a.5.5 0 0 1 .5.5v.776a11.5 11.5 0 0 1-.552 3.519l-1.331 4.14a.5.5 0 0 1-.952-.305l1.33-4.141a10.5 10.5 0 0 0 .504-3.213V7a.5.5 0 0 1 .5-.5Z" />
                                            <path d="M6.06 7a2 2 0 1 1 4 0 .5.5 0 1 1-1 0 1 1 0 1 0-2 0v.332c0 .409-.022.816-.066 1.221A.5.5 0 0 1 6 8.447c.04-.37.06-.742.06-1.115V7Zm3.509 1a.5.5 0 0 1 .487.513 11.5 11.5 0 0 1-.587 3.339l-1.266 3.8a.5.5 0 0 1-.949-.317l1.267-3.8a10.5 10.5 0 0 0 .535-3.048A.5.5 0 0 1 9.569 8Zm-3.356 2.115a.5.5 0 0 1 .33.626L5.24 14.939a.5.5 0 1 1-.955-.296l1.303-4.199a.5.5 0 0 1 .625-.329Z" />
                                            <path d="M4.759 5.833A3.501 3.501 0 0 1 11.559 7a.5.5 0 0 1-1 0 2.5 2.5 0 0 0-4.857-.833.5.5 0 1 1-.943-.334Zm.3 1.67a.5.5 0 0 1 .449.546 10.72 10.72 0 0 1-.4 2.031l-1.222 4.072a.5.5 0 1 1-.958-.287L4.15 9.793a9.72 9.72 0 0 0 .363-1.842.5.5 0 0 1 .546-.449Zm6 .647a.5.5 0 0 1 .5.5c0 1.28-.213 2.552-.632 3.762l-1.09 3.145a.5.5 0 0 1-.944-.327l1.089-3.145c.382-1.105.578-2.266.578-3.435a.5.5 0 0 1 .5-.5Z" />
                                            <path d="M3.902 4.222a4.996 4.996 0 0 1 5.202-2.113.5.5 0 0 1-.208.979 3.996 3.996 0 0 0-4.163 1.69.5.5 0 0 1-.831-.556Zm6.72-.955a.5.5 0 0 1 .705-.052A4.99 4.99 0 0 1 13.059 7v1.5a.5.5 0 1 1-1 0V7a3.99 3.99 0 0 0-1.386-3.028.5.5 0 0 1-.051-.705ZM3.68 5.842a.5.5 0 0 1 .422.568c-.029.192-.044.39-.044.59 0 .71-.1 1.417-.298 2.1l-1.14 3.923a.5.5 0 1 1-.96-.279L2.8 8.821A6.531 6.531 0 0 0 3.058 7c0-.25.019-.496.054-.736a.5.5 0 0 1 .568-.422Zm8.882 3.66a.5.5 0 0 1 .456.54c-.084 1-.298 1.986-.64 2.934l-.744 2.068a.5.5 0 0 1-.941-.338l.745-2.07a10.51 10.51 0 0 0 .584-2.678.5.5 0 0 1 .54-.456Z" />
                                            <path d="M4.81 1.37A6.5 6.5 0 0 1 14.56 7a.5.5 0 1 1-1 0 5.5 5.5 0 0 0-8.25-4.765.5.5 0 0 1-.5-.865Zm-.89 1.257a.5.5 0 0 1 .04.706A5.478 5.478 0 0 0 2.56 7a.5.5 0 0 1-1 0c0-1.664.626-3.184 1.655-4.333a.5.5 0 0 1 .706-.04ZM1.915 8.02a.5.5 0 0 1 .346.616l-.779 2.767a.5.5 0 1 1-.962-.27l.778-2.767a.5.5 0 0 1 .617-.346Zm12.15.481a.5.5 0 0 1 .49.51c-.03 1.499-.161 3.025-.727 4.533l-.07.187a.5.5 0 0 1-.936-.351l.07-.187c.506-1.35.634-2.74.663-4.202a.5.5 0 0 1 .51-.49Z" />
                                        </svg>
                                    </div>
                                    <div class="project-content">
                                        <h6>Aadhar Pay</h6>
                                        <ul>
                                            <li>
                                                <strong>Processing</strong>
                                                <span>0</span>
                                            </li>

                                            <li>
                                                <strong>Completed</strong>
                                                <span>{{post.AadharPay}}</span>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12">
                    <div class="card overflow-hidden project-card">
                        <div class="card-body">
                            <div class="d-flex">
                                <div class="my-auto" style="cursor: pointer" data-bs-toggle="modal" data-bs-target="#UPIdoc">
                                    <svg style="color: blue" xmlns="http://www.w3.org/2000/svg" class="me-4 ht-60 wd-60 my-auto " fill="currentColor" class="bi bi-qr-code-scan" viewBox="0 0 16 16">
                                        <path d="M0 .5A.5.5 0 0 1 .5 0h3a.5.5 0 0 1 0 1H1v2.5a.5.5 0 0 1-1 0v-3Zm12 0a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 .5.5v3a.5.5 0 0 1-1 0V1h-2.5a.5.5 0 0 1-.5-.5ZM.5 12a.5.5 0 0 1 .5.5V15h2.5a.5.5 0 0 1 0 1h-3a.5.5 0 0 1-.5-.5v-3a.5.5 0 0 1 .5-.5Zm15 0a.5.5 0 0 1 .5.5v3a.5.5 0 0 1-.5.5h-3a.5.5 0 0 1 0-1H15v-2.5a.5.5 0 0 1 .5-.5ZM4 4h1v1H4V4Z" fill="blue"></path>
                                        <path d="M7 2H2v5h5V2ZM3 3h3v3H3V3Zm2 8H4v1h1v-1Z" fill="blue"></path>
                                        <path d="M7 9H2v5h5V9Zm-4 1h3v3H3v-3Zm8-6h1v1h-1V4Z" fill="blue"></path>
                                        <path d="M9 2h5v5H9V2Zm1 1v3h3V3h-3ZM8 8v2h1v1H8v1h2v-2h1v2h1v-1h2v-1h-3V8H8Zm2 2H9V9h1v1Zm4 2h-1v1h-2v1h3v-2Zm-4 2v-1H8v1h2Z" fill="blue"></path>
                                        <path d="M12 9h2V8h-2v1Z" fill="blue"></path>
                                    </svg>
                                </div>
                                <div class="project-content">
                                    <h6>QR Code (UPI)</h6>
                                    <ul>
                                        <li>
                                            <strong>Processing</strong>
                                            <span>0</span>
                                        </li>

                                        <li>
                                            <strong>Accepted</strong>
                                            <span>{{post.UPI}}</span>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12">
                    <div class="card overflow-hidden project-card">
                        <div class="card-body">                            
                                <div class="d-flex">
                                    <div class="my-auto">
                                        <svg class="me-4 ht-60 wd-60 my-auto" style="color: blue" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 320 512">
                                            <!--! Font Awesome Free 6.1.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free (Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License) Copyright 2022 Fonticons, Inc. -->
                                            <path d="M.0022 64C.0022 46.33 14.33 32 32 32H288C305.7 32 320 46.33 320 64C320 81.67 305.7 96 288 96H231.8C241.4 110.4 248.5 126.6 252.4 144H288C305.7 144 320 158.3 320 176C320 193.7 305.7 208 288 208H252.4C239.2 266.3 190.5 311.2 130.3 318.9L274.6 421.1C288.1 432.2 292.3 452.2 282 466.6C271.8 480.1 251.8 484.3 237.4 474L13.4 314C2.083 305.1-2.716 291.5 1.529 278.2C5.774 264.1 18.09 256 32 256H112C144.8 256 173 236.3 185.3 208H32C14.33 208 .0022 193.7 .0022 176C.0022 158.3 14.33 144 32 144H185.3C173 115.7 144.8 96 112 96H32C14.33 96 .0022 81.67 .0022 64V64z" fill="blue"></path></svg>
                                    </div>
                                    <div class="project-content">
                                        <h6>Pan Card</h6>
                                        <ul>
                                            <li>
                                                <strong>Processing</strong>
                                                <span>0</span>
                                            </li>
                                            <li>
                                                <strong>Accepted</strong>
                                                <span>{{post.Pan}}</span>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                 <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12" style="cursor: pointer">
                    <div class="card overflow-hidden project-card">
                        <div class="card-body"  data-bs-toggle="modal" data-bs-target="#vanpopup">
                                <div class="d-flex">
                                    <div class="my-auto">
                                        <img class="me-4 ht-60 wd-60 my-auto" src="../images/icon/virtualaccount.png" />
                                    </div>
                                    <div class="project-content">
                                        <h6>Virtual Account</h6>
                                        <ul>
                                            <li>
                                                <strong>Processing</strong>
                                                <span>0</span>
                                            </li>
                                            <li>
                                                <strong>Accepted</strong>
                                                <span>{{post.VanAccount}}</span>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                        </div>
                    </div>
                </div>
                 <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12">
                    <div class="card overflow-hidden project-card">
                        <div class="card-body">
                            <a href="FundRequest.aspx">
                                <div class="d-flex">
                                    <div class="my-auto">
                                        <svg class="me-4 ht-60 wd-60 my-auto" style="color: blue" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 320 512">
                                            <!--! Font Awesome Free 6.1.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free (Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License) Copyright 2022 Fonticons, Inc. -->
                                            <path d="M.0022 64C.0022 46.33 14.33 32 32 32H288C305.7 32 320 46.33 320 64C320 81.67 305.7 96 288 96H231.8C241.4 110.4 248.5 126.6 252.4 144H288C305.7 144 320 158.3 320 176C320 193.7 305.7 208 288 208H252.4C239.2 266.3 190.5 311.2 130.3 318.9L274.6 421.1C288.1 432.2 292.3 452.2 282 466.6C271.8 480.1 251.8 484.3 237.4 474L13.4 314C2.083 305.1-2.716 291.5 1.529 278.2C5.774 264.1 18.09 256 32 256H112C144.8 256 173 236.3 185.3 208H32C14.33 208 .0022 193.7 .0022 176C.0022 158.3 14.33 144 32 144H185.3C173 115.7 144.8 96 112 96H32C14.33 96 .0022 81.67 .0022 64V64z" fill="blue"></path></svg>
                                    </div>
                                    <div class="project-content">
                                        <h6>Fund Request</h6>
                                        <ul>
                                            <li>
                                                <strong>Processing</strong>
                                                <span>0</span>
                                            </li>
                                            <li>
                                                <strong>Accepted</strong>
                                                <span>{{post.FundRequest}}</span>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
								
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row row-sm ">
                <div class="col-xl-6 col-lg-12 col-md-12 col-sm-12">
                    <div class="card overflow-hidden">
                        <div class="card-header bg-transparent pd-b-0 pd-t-20 bd-b-0">
                            <div class="d-flex justify-content-between">
                                <h4 class="card-title mg-b-10">Search Transaction</h4>
                                <i class="mdi mdi-dots-horizontal text-gray"></i>
                            </div>
                        </div>
                        <div class="card-body pd-y-7">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" v-model="TransID" ID="txtSearchTransaction" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <input type="button" data-bs-target="#findtrans" data-bs-toggle="modal" value="Search" class="btn btn-danger" v-on:click="FindTransaction" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6 col-lg-12 col-md-12 col-sm-12">
                    <div class="card overflow-hidden">
                        <div class="card-header bg-transparent pd-b-0 pd-t-20 bd-b-0">
                            <div class="d-flex justify-content-between">
                                <h4 class="card-title mg-b-10">KYC Status</h4>
                                <i class="mdi mdi-dots-horizontal text-gray"></i>
                            </div>
                        </div>
                        <div class="card-body pd-y-7">
                            <div class="row">
                                <div class="col-md-4">
                                    <a href='<%= (Convert.ToBoolean(dtMember.Rows[0]["IsKycApproved"])?"#":"uploadkyc.aspx") %>'>
                                        <img width="100" src='<%= "../images/icon/"+(Convert.ToBoolean(dtMember.Rows[0]["IsKycApproved"])?"verified.png":"nonkyc.png") %>' />
                                    </a>
                                </div>
                                <div class="col-md-4">
                                    <a href="ActiveYourPlan.aspx">
                                        <img src="../images/icon/upgrade.png" />
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- row -->
            <div class="row row-sm ">
                <div class="col-xl-6 col-lg-12 col-md-12 col-sm-12">
                    <div class="card overflow-hidden">
                        <div class="card-header bg-transparent pd-b-0 pd-t-20 bd-b-0">
                            <div class="d-flex justify-content-between">
                                <h4 class="card-title mg-b-10">News</h4>
                                <i class="mdi mdi-dots-horizontal text-gray"></i>
                            </div>
                        </div>
                        <div class="card-body pd-y-7">
                            <asp:Repeater runat="server" ID="rptDataNews">
                                <ItemTemplate>
                                    <marquee direction="up" onmouseover="this.stop()" onmouseout="this.start()" style="marquee-speed: low"><%# Eval("Description") %></marquee>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12 col-lg-12 col-xl-6">
                    <div class="card overflow-hidden">

                        <div class="card-body ht-100p">

                            <div>
                                <div class="carousel slide" data-bs-ride="carousel" id="carouselExample4">
                                    <ol class="carousel-indicators">
                                        <li class="" data-bs-slide-to="0" data-bs-target="#carouselExample4"></li>
                                        <li data-bs-slide-to="1" data-bs-target="#carouselExample4" class=""></li>
                                        <li data-bs-slide-to="2" data-bs-target="#carouselExample4" class="active" aria-current="true"></li>
                                    </ol>
                                    <div class="carousel-inner bg-dark">
                                        <asp:Repeater runat="server" ID="rptDashBoardBanner">
                                            <ItemTemplate>
                                                <div class='<%# (Container.ItemIndex.ToString()=="0"?"carousel-item active":"carousel-item") %>'>
                                                    <img alt="img" class="d-block w-100" src='<%# "../images/Banner/"+ Eval("ImagePath").ToString()%>'>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /row -->



            <!-- row -->
            <div class="row row-sm ">
                <div class="col-md-12 col-xl-12">
                    <div class="card overflow-hidden review-project">
                        <div class="card-body">
                            <div class="d-flex justify-content-between">
                                <h4 class="card-title mg-b-10">Last Transaction</h4>
                                <i class="mdi mdi-dots-horizontal text-gray"></i>
                            </div>
                            <div class="table-responsive mb-0">
                                <table class="table table-hover table-bordered mb-0 text-md-nowrap text-lg-nowrap text-xl-nowrap table-striped ">
                                    <thead>
                                        <tr>
                                            <th>Service</th>
                                            <th>AMOUNT</th>
                                            <th>BALANCE</th>
                                            <th>Description</th>
                                            <th>Status</th>
                                            <th>TransferDate</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptData">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <div class="project-contain">
                                                            <h6 class="mb-1 tx-13"><%#Eval("ServiceName") %></h6>
                                                        </div>
                                                    </td>

                                                    <td><%#Eval("AMOUNT") %></td>
                                                    <td><%#Eval("BALANCE") %></td>
                                                    <td><%#Eval("Description") %></td>
                                                    <td>
                                                        <span class='<%# (Eval("factor").ToString().ToUpper()=="CR"?"btn btn-success":"btn btn-danger") %>'>
                                                            <%#Eval("FACTOR") %>
                                                        </span>
                                                    </td>
                                                    <td><%#Eval("ADDDATE") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /row -->

            <!-- row -->
            <div class="row row-sm " style="display: none">
                <div class="col-lg-12 col-xl-4 col-sm-12">
                    <div class="card">
                        <div class="card-header pb-0 pt-4">
                            <div class="d-flex justify-content-between">
                                <h4 class="card-title mg-b-10">Top Ongoing Projects</h4>
                                <i class="mdi mdi-dots-horizontal text-gray"></i>
                            </div>
                            <p class="tx-12 text-muted mb-0">Project Description is a formally written declaration of the project and its idea and context . <a href="">Learn more</a></p>
                        </div>
                        <div class="card-body p-0 m-scroll mh-350 mt-2">
                            <div class="list-group projects-list">
                                <a href="javascript:void(0);" class="list-group-item list-group-item-action flex-column align-items-start border-top-0">
                                    <div class="d-flex w-100 justify-content-between">
                                        <h6 class="mb-1 font-weight-semibold ">PSD Projects</h6>
                                        <small class="text-danger"><i class="fa fa-caret-down me-1"></i>5 days ago</small>
                                    </div>
                                    <p class="mb-0 text-muted mb-0 tx-12">Started:17-02-2020</p>
                                    <small class="text-muted">Lorem ipsum dolor sit amet, consectetuer adipiscing elit...</small>
                                </a>
                                <a href="javascript:void(0);" class="list-group-item list-group-item-action flex-column align-items-start">
                                    <div class="d-flex w-100 justify-content-between">
                                        <h6 class="mb-1 font-weight-semibold">Wordpress Projects</h6>
                                        <small class="text-success"><i class="fa fa-caret-up me-1"></i>2 days ago</small>
                                    </div>
                                    <p class="mb-0 text-muted mb-0 tx-12">Started:15-02-2020</p>
                                    <small class="text-muted">Lorem ipsum dolor sit amet, consectetuer adipiscing elit..</small>
                                </a>
                                <a href="javascript:void(0);" class="list-group-item list-group-item-action flex-column align-items-start">
                                    <div class="d-flex w-100 justify-content-between">
                                        <h6 class="mb-1 font-weight-semibold">HTML &amp; CSS3 Projects</h6>
                                        <small class="text-danger"><i class="fa fa-caret-down me-1"></i>1 days ago</small>
                                    </div>
                                    <p class="mb-0 text-muted mb-0 tx-12">Started:26-02-2020</p>
                                    <small class="text-muted">Lorem ipsum dolor sit amet, consectetuer adipiscing elit..</small>
                                </a>
                                <a href="javascript:void(0);" class="list-group-item list-group-item-action flex-column align-items-start">
                                    <div class="d-flex w-100 justify-content-between">
                                        <h6 class="mb-1 font-weight-semibold">HTML &amp; CSS3 Projects</h6>
                                        <small class="text-danger"><i class="fa fa-caret-down me-1"></i>1 days ago</small>
                                    </div>
                                    <p class="mb-0 text-muted mb-0 tx-12">Started:26-02-2020</p>
                                    <small class="text-muted">Lorem ipsum dolor sit amet, consectetuer adipiscing elit..</small>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-4 col-lg-6 col-md-12">
                    <div class="card overflow-hidden ">
                        <div class="card-header pb-0 pt-4">
                            <div class="d-flex justify-content-between">
                                <h4 class="card-title mg-b-10 ">Activity</h4>
                                <i class="mdi mdi-dots-horizontal text-gray"></i>
                            </div>
                            <p class="tx-12 text-muted mb-0">An activity is a scheduled phase in a project plan with a distinct beginning and end. <a href="">Learn more</a></p>
                        </div>
                        <div class="card-body p-0">
                            <div class="activity Activity-scroll">
                                <div class="activity-list">
                                    <img src="../assets/img/faces/6.jpg" alt="" class="img-activity">
                                    <div class="time-activity ">
                                        <div class="item-activity">
                                            <p class="mb-0"><span class="h6 me-1">Adam Berry</span><span class="text-muted tx-13"> Add a new projects</span> <span class="h6 ms-1 added-project">AngularJS Template</span></p>
                                            <small class="text-muted ">30 mins ago</small>
                                        </div>
                                    </div>
                                    <img src="../assets/img/faces/9.jpg" alt="" class="img-activity">
                                    <div class="time-activity">
                                        <div class="item-activity">
                                            <p class="mb-0"><span class="h6 me-1">Irene Hunter</span> <span class="text-muted tx-13">Add a new projects</span> <span class="h6 ms-1 added-project text-danger">Free HTML Template</span></p>
                                            <small class="text-muted ">1 days ago</small>
                                        </div>
                                    </div>
                                    <img src="../assets/img/faces/3.jpg" alt="" class="img-activity">
                                    <div class="time-activity">
                                        <div class="item-activity">
                                            <p class="mb-0"><span class="h6 me-1">John Payne</span> <span class="text-muted tx-13">Add a new projects</span> <span class="h6 ms-1 added-project text-success">Free PSD Template</span></p>
                                            <small class="text-muted ">3 days ago</small>
                                        </div>
                                    </div>
                                    <img src="../assets/img/faces/4.jpg" alt="" class="img-activity">
                                    <div class="time-activity">
                                        <div class="item-activity ">
                                            <p class="mb-0"><span class="h6 me-1">Julia Hardacre</span> <span class="text-muted tx-13">Add a new projects</span> <span class="h6 ms-1 added-project text-warning">Free UI Template</span></p>
                                            <small class="text-muted ">5 days ago</small>
                                        </div>
                                    </div>
                                    <img src="../assets/img/faces/5.jpg" alt="" class="img-activity">
                                    <div class="time-activity">
                                        <div class="item-activity">
                                            <p class="mb-0"><span class="h6 me-1">Adam Berry</span> <span class="text-muted tx-13">Add a new projects</span> <span class="h6 ms-1 added-project text-pink">AngularJS Template</span></p>
                                            <small class="text-muted ">30 mins ago</small>
                                        </div>
                                    </div>
                                    <img src="../assets/img/faces/6.jpg" alt="" class="img-activity">
                                    <div class="time-activity">
                                        <div class="item-activity">
                                            <p class="mb-0"><span class="h6 me-1">Irene Hunter</span> <span class="text-muted tx-13">Add a new projects</span> <span class="h6 ms-1 added-project text-purple">Free HTML Template</span></p>
                                            <small class="text-muted ">1 days ago</small>
                                        </div>
                                    </div>
                                    <img src="../assets/img/faces/16.jpg" alt="" class="img-activity">
                                    <div class="time-activity">
                                        <div class="item-activity">
                                            <p class="mb-0"><span class="h6 me-1">John Payne</span><span class="text-muted tx-13"> Add a new projects</span> <span class="h6 ms-1 added-project text-success">Free PSD Template</span></p>
                                            <small class="text-muted ">3 days ago</small>
                                        </div>
                                    </div>
                                    <img src="../assets/img/faces/10.jpg" alt="" class="img-activity">
                                    <div class="time-activity mb-0">
                                        <div class="item-activity mb-0">
                                            <p class="mb-0"><span class="h6 me-1">Julia Hardacre</span><span class="text-muted tx-13"> Add a new projects</span><span class="h6 ms-1 added-project">Free UI Template</span></p>
                                            <small class="text-muted ">5 days ago</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-lg-6 col-xl-4">
                    <div class="card">
                        <div class="card-header pt-4 pb-0">
                            <div class="d-flex justify-content-between">
                                <h4 class="card-title mg-b-10 ">Task Statistics</h4>
                                <i class="mdi mdi-dots-horizontal text-gray"></i>
                            </div>
                            <p class="tx-12 text-muted mb-0">The Statistics You can also summarize your data in a graphical display, such as histograms <a href="">Learn more</a> </p>
                        </div>
                        <div class="p-4">
                            <div class="">
                                <div class="row">
                                    <div class="col-md-6 col-6 text-center">
                                        <div class="task-box primary mb-0">
                                            <p class="mb-0 tx-12">Total Tasks</p>
                                            <h3 class="mb-0">385</h3>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-6 text-center">
                                        <div class="task-box danger  mb-0">
                                            <p class="mb-0 tx-12">Overdue Tasks</p>
                                            <h3 class="mb-0">19</h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="task-stat pb-0">
                            <div class="d-flex tasks">
                                <div class="mb-0">
                                    <div class="h6 fs-15 mb-0"><i class="far fa-dot-circle text-primary me-2"></i>Completed Tasks</div>
                                    <span class="text-muted tx-11 ms-4">8:00am - 10:30am</span>
                                </div>
                                <span class="float-end ms-auto">135</span>
                            </div>
                            <div class="d-flex tasks">
                                <div class="mb-0">
                                    <div class="h6 fs-15 mb-0"><i class="far fa-dot-circle text-pink me-2"></i>Inprogress Tasks</div>
                                    <span class="text-muted tx-11 ms-4">8:00am - 10:30am</span>
                                </div>
                                <span class="float-end ms-auto">75</span>
                            </div>
                            <div class="d-flex tasks">
                                <div class="mb-0">
                                    <div class="h6 fs-15 mb-0"><i class="far fa-dot-circle text-success me-2"></i>On Hold Tasks</div>
                                    <span class="text-muted tx-11 ms-4">8:00am - 10:30am</span>
                                </div>
                                <span class="float-end ms-auto">23</span>
                            </div>
                            <div class="d-flex tasks mb-0 border-bottom-0">
                                <div class="mb-0">
                                    <div class="h6 fs-15 mb-0"><i class="far fa-dot-circle text-purple me-2"></i>Pending Tasks</div>
                                    <span class="text-muted tx-11 ms-4">8:00am - 10:30am</span>
                                </div>
                                <span class="float-end ms-auto">1</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal" id="findtrans" aria-modal="true" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content modal-content-demo">
                        <div class="modal-header">
                            <h6 class="modal-title">Transaction History</h6>
                            <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button"><span aria-hidden="true">×</span></button>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <table class="border-top-0  table table-bordered text-nowrap key-buttons border-bottom">
                                    <thead>
                                        <tr>
                                            <th class="border-bottom-0">SNO</th>
                                            <th class="border-bottom-0">MemberID</th>
                                            <th class="border-bottom-0">Factor</th>
                                            <th class="border-bottom-0">Amount</th>
                                            <th class="border-bottom-0">Transaction Id</th>
                                            <th class="border-bottom-0">Narration</th>
                                            <th class="border-bottom-0">Description</th>
                                            <th class="border-bottom-0">Add Date</th>
                                        </tr>
                                    </thead>
                                    <tbody>

                                        <tr class="row1" v-for="(item,index) in TransData">
                                            <td>{{index+1}}</td>
                                            <td>{{item.LOGINID}}</td>
                                            <td>{{item.FACTOR}}<%#Eval("Factor") %></td>
                                            <td>{{item.AMOUNT}}<%#Eval("Amount") %></td>
                                            <td>{{item.TRANSACTIONID}}<%#Eval("TRANSACTIONID") %></td>
                                            <td>{{item.NARRATION}}<%#Eval("NARRATION") %></td>
                                            <td>{{item.Description}}<%#Eval("Description") %></td>
                                            <td>{{item.ADDDATE}}</td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button class="btn ripple btn-secondary" data-bs-dismiss="modal" type="button">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /row -->
        <asp:HiddenField runat="server" ID="hdnAuth" ClientIDMode="Static" Value="0" />
    </div>

     <div class="modal upi-modal" id="vanpopup" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Add Funds to <%= company.Name %></h5>
                       <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button"><span aria-hidden="true">×</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="upi-section">
                            <div class="float-end"></div>
                            <div class="upi-head">
                                Fund Using Online Transfer/ UPI
                            </div>
                            <p>
                                You can fund your account by transferring the amount to your  <%= company.Name %>
                            </p>


                            <div class="upi-head">
                                Account Details :
                            </div>
                            <div class="company-details">
                                <div class="row">
                                    <div class="col-md-7">
                                        <ul>
                                            <li class="d-flex"><span>Company Name </span><span class="company-heading fw-600"><%= company.Name %></span></li>
                                            <li><span>Account No </span><span class="company-heading fw-600"><%= "PNKI"+dtMember.Rows[0]["Mobile"].ToString() %></span></li>
                                            <li><span>IFSC </span><span class="company-heading fw-600">ICIC0000104</span></li>

                                            <li><span>Modes </span>UPI/IMPS/NEFT/RTGS</li>

                                        </ul>
                                    </div>

                                </div>

                            </div>
                            <p class="mt-3">if the money is transfered through IMPS/UPI,the amount will be reflectedin your <%= company.Name %> account within 5 mins</p>
                            <p>if the money is transfered through IMPS/UPI,the amount will be reflectedin your <%= company.Name %> account within 1 working Hours</p>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    <!-- /container -->
    <!-- /main-content -->

    <script>

        new Vue({
            el: '#app1',
            data() {
                return {
                    MainData: [],
                    MainDataCount: [],
                    TransData: [],
                    Search: "",
                    TransID: "",
                    Datex: ""
                }
            },
            methods: {
                Getdate() {
                    var today = new Date();
                    var dd = String(today.getDate()).padStart(2, '0');
                    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
                    var yyyy = today.getFullYear();

                    today = yyyy + '-' + mm + '-' + dd;
                    return today;
                },
                init() {
                    var el = this;
                    const article = {
                        "MethodName": "getflight",
                        "Data": el.Datex,
                        "Auth": $("#hdnAuth").val()
                    }
                    axios.post("Dashboard.aspx/GetData", article)
                        .then(response => {
                            this.MainData = response.data.d
                            this.initCount();
                        });// .then(function (response) { debugger; this.MainData =response.data.d; });
                },
                initCount() {
                    var el = this;
                    const article = {
                        "MethodName": "getflight",
                        "Data": el.Datex
                    }
                    axios.post("Dashboard.aspx/GetDataCount", article)
                        .then(response => {
                            this.MainDataCount = JSON.parse(response.data.d);
                        });// .then(function (response) { debugger; this.MainData =response.data.d; });
                },
                FindTransaction() {
                    var el = this;
                    el.TransData = [];
                    const article = {
                        "TransID": el.TransID
                    }
                    axios.post("Dashboard.aspx/FindTransaction", article)
                        .then(response => {
                            this.TransData = JSON.parse(response.data.d);
                        });// .then(function (response) { debugger; this.MainData =response.data.d; });
                }
            },
            mounted() {
                try {
                    this.Datex = this.Getdate();
                    this.init();
                } catch (err) {

                }

            },


        });
    </script>
</asp:Content>

