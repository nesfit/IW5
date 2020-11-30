import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IngredientDetailModel } from '../api/models';
import { IngredientService } from '../api/services';

@Component({
  selector: 'app-ingredient',
  templateUrl: './ingredient.component.html',
  styleUrls: ['./ingredient.component.less']
})
export class IngredientComponent implements OnInit {

  constructor(private ingredientService: IngredientService, private route: ActivatedRoute) { }

  errorMessage = '';
  ingredient: IngredientDetailModel | undefined;
  id: string = "0";

  ngOnInit(): void {
    this.id = this.route.snapshot.params.id;
  }
}
