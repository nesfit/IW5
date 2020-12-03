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

  constructor(
    private ingredientService: IngredientService,
    private route: ActivatedRoute
  ) { }
  loadIngredientError = false;
  editingAvailable = false;
  errorMessage = '';
  model: IngredientDetailModel = { description: '', name: '' };

  ngOnInit(): void {
    const id = this.route.snapshot.params.id;
    if (id !== undefined) {
      const request = { id };

      this.ingredientService.ingredientGetById(request).subscribe(
        providedIngredient => this.model = providedIngredient,
        error => this.loadIngredientError = true);

      this.editingAvailable = true;
    }
  }

  onSave(): void {
    if (this.model !== undefined) {
      const model = this.model;
      const request = { body: model };

      if (model.id !== undefined) {
        this.ingredientService.ingredientUpdate(request).subscribe();
      }
      else {
        this.ingredientService.ingredientCreate(request).subscribe();
        this.model = { description: '', name: '' };
      }
    }
  }

  onDelete(): void {
    if (this.model?.id !== undefined) {
      const id: string = this.model.id;
      const request = { id };

      this.ingredientService.ingredientDelete(request).subscribe();
    }
  }
}
