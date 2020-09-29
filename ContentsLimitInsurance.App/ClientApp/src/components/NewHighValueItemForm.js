import React from "react";
import { connect } from "react-redux";
import { addHighValueItemRequest } from "./../store/thunks";
import { Card, Form, Row, Col, Button } from "react-bootstrap";

class NewHighValueItemForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      name: "",
      value: 0,
      itemCategoryId: 1,
    };

    this.handleInputChange = this.handleInputChange.bind(this);
  }

  handleInputChange(event) {
    const target = event.target;
    const value = target.type === "checkbox" ? target.checked : target.value;
    const name = target.name;

    this.setState({
      [name]: value,
    });
  }

  render() {
    return (
      <Card>
        <Card.Body>
          <Form>
            <Form.Group as={Row} controlId="formPlaintextEmail">
              <Col sm="5">
                <Form.Control
                  name="name"
                  type="text"
                  maxLength="50"
                  placeholder="Item Name"
                  value={this.state.name}
                  onChange={this.handleInputChange}
                />
              </Col>
              <Col sm="2">
                <Form.Control
                  name="value"
                  type="number"
                  placeholder="Value"
                  maxLength="6"
                  title="Value"
                  value={this.state.value}
                  onChange={this.handleInputChange}
                />
              </Col>
              <Col sm="4">
                <Form.Control
                  as="select"
                  name="itemCategoryId"
                  title="Category"
                  value={this.state.itemCategoryId}
                  onChange={this.handleInputChange}
                  defaultValue="Eletronics"
                >
                  <option value="1">Eletronics</option>
                  <option value="2">Clothing</option>
                  <option value="3">Kitchen</option>
                </Form.Control>
              </Col>
              <Col sm="1">
                <Button
                  onClick={() => {
                    const isDuplicateText = false;
                    if (!isDuplicateText) {
                      this.props.onCreatePressed(
                        this.state.name,
                        this.state.value,
                        this.state.itemCategoryId
                      );
                      this.setState({
                        name: "",
                        value: 0,
                        itemCategoryId: 1,
                      });
                    }
                  }}
                >
                  Add
                </Button>
              </Col>
            </Form.Group>
          </Form>
        </Card.Body>
      </Card>
    );
  }
}
const mapStateToProps = (state) => ({
  items: state.items,
});

const mapDispatchToProps = (dispatch) => ({
  onCreatePressed: (name, value, itemCategoryId) =>
    dispatch(addHighValueItemRequest(name, value, itemCategoryId)),
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(NewHighValueItemForm);
