import React from "react";
import { connect } from "react-redux";
import "./Category.css";
import HighValueItem from "./HighValueItem";
import { removeHighValueItemRequest } from "../store/thunks";
import { Card, Table, Row, Col } from "react-bootstrap";

const Category = ({ category, items, onRemovePressed }) => {
  const content = (
    <Card>
      <Card.Body>
        <Card.Title>
          <Row>
            <Col lg={8} md={8} sm={6}>
              {category.name}
            </Col>
            <Col lg={4} md={4} sm={6}>
              $
              {items.reduce(function (prev, current) {
                return prev + +current.value;
              }, 0)}
            </Col>
          </Row>
        </Card.Title>
        <Card.Body>
          <Table className="table-hover" size="sm">
            <tbody>
              {items.map((item) => (
                <HighValueItem
                  key={item.highValueItemId}
                  item={item}
                  onRemovePressed={onRemovePressed}
                />
              ))}
            </tbody>
          </Table>
        </Card.Body>
      </Card.Body>
    </Card>
  );

  return items.length > 0 ? content : null;
};

const mapStateToProps = (state) => ({});

const mapDispatchToProps = (dispatch) => ({
  onRemovePressed: (highValueItemId) =>
    dispatch(removeHighValueItemRequest(highValueItemId)),
});

export default connect(mapStateToProps, mapDispatchToProps)(Category);
