import React, { Component } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Layout from "./components/Layout";
import Home from "./components/Home";
import HighValueItemsList from "./components/HighValueItemsList";
import "./custom.css";

export default class App extends Component {
  render() {
    return (
      <Layout>
        <Router basename="/contents-limit-insurance">
          <Switch>
            <Route exact path="/" component={Home} />
          </Switch>
        </Router>
      </Layout>
    );
  }
}
