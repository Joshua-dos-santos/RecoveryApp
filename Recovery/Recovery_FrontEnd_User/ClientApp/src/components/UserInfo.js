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
        this.OnUpdateUserData = this.OnUpdateUserData.bind(this);
    }

    componentDidUpdate() { }

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

    OnUpdateUserData = (e) => {
        var self = this;
        const user = self.state.userData;
        axios({
            method: "POST",
            url: "http://localhost:5000/Account/UpdateUser/UpdateUser",
            dataType: "json",
            data:  user ,
        }).then((data) => {
            console.log(data);
            return;
        });
    };

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
                            <Label for="last_Name">Last Name</Label>
                            <Input
                                type="last_Name"
                                placeholder={this.state.userData.last_Name}
                                onChange={(e) =>
                                    (this.state.userData.last_Name = e.target.value)
                                }
                                name="last_Name"
                            />
                        </div>
                        <div className="py-2">
                            <Label for="email">Email</Label>
                            <Input
                                type="email"
                                placeholder={this.state.userData.email}
                                onChange={(e) =>
                                    (this.state.userData.email = e.target.value)
                                }
                                name="email"
                            />
                        </div>
                        <div className="py-2">
                            <Label for="height">Height</Label>
                            <Input
                                type="height"
                                placeholder={this.state.userData.height}
                                onChange={(e) =>
                                    (this.state.userData.height = e.target.value)
                                }
                                name="height"
                            />
                        </div>
                        <div className="py-2">
                            <Label for="weight">Weight</Label>
                            <Input
                                type="weight"
                                placeholder={this.state.userData.weight}
                                onChange={(e) =>
                                    (this.state.userData.weight = e.target.value)
                                }
                                name="weight"
                            />
                        </div>
                        <Button
                            className="btn btn-primary"
                            onClick={(e) => this.OnUpdateUserData(e)}
                        >
                            Update User
                        </Button>
                    </div>
                </div>
            </body>
        )
    }
}