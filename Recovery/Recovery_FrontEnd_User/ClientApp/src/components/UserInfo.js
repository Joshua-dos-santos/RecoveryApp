import * as React from "react";
import { Component } from "react";
import { NavMenu } from "../components/NavMenu";
import Label from "reactstrap/lib/Label";
import axios from "axios";
import { Link, Redirect } from "react-router-dom";
import { Input } from "reactstrap";
import Button from "reactstrap/lib/Button";

export class UserInfo extends Component {
    static displayName = UserInfo.name;

    constructor(props) {
        super(props);

        this.state = {
            loading: true,
            userData: [],
            jtoken: sessionStorage.getItem("token"),
            loggedIn: false
        };
        this.OnLoad = this.OnLoad.bind(this);
    }

    componentDidMount() {
        this.OnLoad();
    }

    OnLoad(e) {
        var self = this;
        console.log(sessionStorage.getItem("token"))
        axios({
            method: 'GET',
            url: 'http://localhost:5000/Account/GetUserByToken/GetUserByToken',
            params: {
                jtoken: sessionStorage.getItem("token")
            }
        }).then((data) => {
            console.log(data);
            self.setState({ userData: data.data }, () => { console.log(self.state.userData) });
        });
    }

    render() {
        if (!sessionStorage.getItem("loggedin")) {
            return (
                <Redirect to="/login" />
            )
        }
        return (
            <body>
                <div class="card mx-auto" style={{ backgroundColor: "white", maxWidth: "60%" }}>
                    <div class="card-body">
                        <h4 style={{ textAlign: "center", fontWeight: "bold" }} class="card-title">
                            User information
                        </h4>
                        <div className="py-2">
                            <Label for="fullName">First Name</Label>
                            <Input
                                type="first_Name"
                                placeholder={this.state.userData.first_Name}
                                onChange={(e) =>
                                    (this.state.userData.first_Name = e.target.value)
                                }
                                name="first_Name"
                            />
                        </div>
                        <div className="py-2">
                            <Label for="fullName">Last Name</Label>
                            <Input
                                type="first_Name"
                                placeholder={this.state.userData.last_Name}
                                onChange={(e) =>
                                    (this.state.userData.last_Name = e.target.value)
                                }
                                name="last_Name"
                            />
                        </div>
                    </div>
                </div>
            </body>
        )
    }
}