import React, { Component } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Layout from "./components/Layout";
import Home from "./components/Home";
import HighValueItemsList from "./components/HighValueItemsList";
import history from "./store/history";
import "./custom.css";

export default class App extends Component {
  render() {
    return (
      <Layout>
        <Router basename="/contents-limit-insurance">
          <Switch>
            <Route exact path="/" component={Home} />
            <Route
              path="/high-value-items-list/:startDateIndex?"
              component={HighValueItemsList}
            />
          </Switch>
        </Router>
      </Layout>
    );
  }
}
